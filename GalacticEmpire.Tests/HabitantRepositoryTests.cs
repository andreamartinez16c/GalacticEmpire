using FluentAssertions;
using GalacticEmpire.Controllers;
using GalacticEmpire.Data;
using GalacticEmpire.Models.DTOs;
using GalacticEmpire.Models.Entities;
using GalacticEmpire.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GalacticEmpire.Tests
{
    public class HabitantRepositoryTests
    {
        private readonly IHabitantRepository _repo;
        private readonly GalacticEmpireContext _context;

        public HabitantRepositoryTests()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<GalacticEmpireContext>(options =>
                options.UseInMemoryDatabase(Guid.NewGuid().ToString())); // New unique in-memory database for each test
            serviceCollection.AddTransient<IHabitantRepository, HabitantRepository>();
            var serviceProvider = serviceCollection.BuildServiceProvider();
            _context = serviceProvider.GetRequiredService<GalacticEmpireContext>();
            _repo = serviceProvider.GetRequiredService<IHabitantRepository>();
        }

        [Fact]
        public async Task GetHabitantsBySpecieNameAsync_ShouldReturnHabitants_WhenHabitantsExistForSpecie()
        {
            // Arrange
            var specie = new Specie { IdSpecie = 1, Name = "Human" };
            var planet = new Planet { IdPlanet = 1, Name = "Earth" };
            var habitant = new Habitant { Name = "John", IdSpecie = 1, IdPlanetOfOrigin = 1, IsRebel = false };

            _context.Species.Add(specie);
            _context.Planets.Add(planet);
            _context.Habitants.Add(habitant);
            await _context.SaveChangesAsync();

            // Act
            var habitants = await _repo.GetHabitantsBySpecieNameAsync("Human");

            // Assert
            habitants.Should().NotBeEmpty().And.HaveCount(1);
            habitants.First().Name.Should().Be("John");
        }

        [Fact]
        public async Task AddHabitantAsync_ShouldAddHabitantToDatabase()
        {
            // Arrange
            _context.Species.Add(new Specie { IdSpecie = 1, Name = "Human" });
            _context.Planets.Add(new Planet { IdPlanet = 1, Name = "Earth" });
            await _context.SaveChangesAsync();

            var habitantDto = new HabitantDto
            {
                Name = "Luke",
                SpecieName = "Human",
                PlanetName = "Earth",
                IsRebel = true
            };

            // Act
            await _repo.AddAsync(habitantDto);
            await _context.SaveChangesAsync();

            // Assert
            var habitantInDb = await _context.Habitants.FirstOrDefaultAsync(h => h.Name == "Luke");
            habitantInDb.Should().NotBeNull();
            habitantInDb.IsRebel.Should().BeTrue();
            habitantInDb.Name.Should().Be("Luke");
        }
    }
}
