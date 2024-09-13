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
    public class PlanetRepositoryTests
    {
        private readonly IPlanetRepository _repo;
        private readonly GalacticEmpireContext _context;

        public PlanetRepositoryTests()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<GalacticEmpireContext>(options =>
                options.UseInMemoryDatabase("GalacticEmpireTestDatabase"));
            serviceCollection.AddTransient<IPlanetRepository, PlanetRepository>();
            var serviceProvider = serviceCollection.BuildServiceProvider();
            _context = serviceProvider.GetRequiredService<GalacticEmpireContext>();
            _repo = serviceProvider.GetRequiredService<IPlanetRepository>();
        }

        [Fact]
        public async Task AddPlanetAsync_ShouldAddPlanetToDatabase()
        {
            // Arrange
            var planetDto = new PlanetDTO { Name = "Mars" };

            // Act
            await _repo.AddAsync(planetDto);
            await _context.SaveChangesAsync();

            // Assert
            var planetInDb = await _context.Planets.FirstOrDefaultAsync(p => p.Name == "Mars");
            planetInDb.Should().NotBeNull();
            planetInDb.Name.Should().Be(planetDto.Name);
        }

        [Fact]
        public async Task GetAllPlanetsAsync_ShouldReturnAllPlanets()
        {
            // Arrange
            _context.Planets.Add(new Planet { IdPlanet = 1, Name = "Venus" });
            await _context.SaveChangesAsync();

            // Act
            var planets = await _repo.GetAllAsync();

            // Assert
            planets.Should().NotBeEmpty().And.HaveCount(1);
            planets.First().Name.Should().Be("Venus");
        }
    }
}
