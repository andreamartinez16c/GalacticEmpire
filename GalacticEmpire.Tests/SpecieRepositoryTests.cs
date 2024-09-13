using FluentAssertions;
using GalacticEmpire.Data;
using GalacticEmpire.Models.DTOs;
using GalacticEmpire.Models.Entities;
using GalacticEmpire.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Tests
{
    public class SpecieRepositoryTests
    {
        private readonly ISpecieRepository _repo;
        private readonly GalacticEmpireContext _context;

        public SpecieRepositoryTests()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<GalacticEmpireContext>(options =>
                options.UseInMemoryDatabase(Guid.NewGuid().ToString())); // Usar un Guid para asegurar que cada test tiene su propia DB en memoria
            serviceCollection.AddTransient<ISpecieRepository, SpecieRepository>();
            var serviceProvider = serviceCollection.BuildServiceProvider();
            _context = serviceProvider.GetRequiredService<GalacticEmpireContext>();
            _repo = serviceProvider.GetRequiredService<ISpecieRepository>();
        }

        [Fact]
        public async Task GetAllSpeciesAsync_ShouldReturnEmpty_WhenNoSpeciesExist()
        {
            // Assert that the database is empty
            var species = await _repo.GetAllAsync();
            species.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllSpeciesAsync_ShouldReturnSpecies_WhenSpeciesExist()
        {
            // Arrange
            _context.Species.Add(new Specie { IdSpecie = 1, Name = "Human" });
            await _context.SaveChangesAsync();

            // Act
            var species = await _repo.GetAllAsync();

            // Assert
            species.Should().NotBeEmpty().And.HaveCount(1);
            species.First().Name.Should().Be("Human");
        }

        [Fact]
        public async Task AddSpeciesAsync_ShouldAddSpeciesToDatabase()
        {
            // Arrange
            var specieDto = new SpecieDto { Name = "Alien" };

            // Act
            await _repo.AddAsync(specieDto);
            await _context.SaveChangesAsync();

            // Assert
            var speciesInDb = await _context.Species.FirstOrDefaultAsync(s => s.Name == "Alien");
            speciesInDb.Should().NotBeNull();
            speciesInDb.Name.Should().Be(specieDto.Name);
        }
    }
}
