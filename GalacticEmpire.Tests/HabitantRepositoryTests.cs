using GalacticEmpire.Controllers;
using GalacticEmpire.Data;
using GalacticEmpire.Models;
using GalacticEmpire.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly HabitantRepository _repo;
        private readonly GalacticEmpireContext _context;

        public HabitantRepositoryTests()
        {
            _context = new GalacticEmpireContext(new DbContextOptionsBuilder<GalacticEmpireContext>()
                .UseInMemoryDatabase(databaseName: "GalacticEmpireDB")
                .Options);
            _repo = new HabitantRepository(_context);
        }

        [Fact]
        public async Task Assert_GetHabitantsAsync()
        {
            _context.Habitants.Add(new Habitant { Id = 2, Name = "C-3PO", IsRebel = false });
            await _context.SaveChangesAsync();
            List<Habitant> habitants = (List<Habitant>)await _repo.GetAllHabitantsAsync();
            Assert.NotEmpty(habitants);
            Assert.IsType<Habitant>(habitants.FirstOrDefault());
        }

        [Fact]
        public async Task Assert_GetSpeciesAsync()
        {
            _context.Species.Add(new Specie { IdSpecie = 1, Name = "Human" });
            await _context.SaveChangesAsync();
            List<Specie> species = await _repo.GetSpeciesAsync();
            Assert.NotEmpty(species);
            Assert.IsType<Specie>(species.FirstOrDefault());
        }

        [Fact]
        public async Task Assert_GetPlanetsAsync()
        {
            _context.Planets.AddRange(new Planet { IdPlanet = 1, Name = "Tatooine" });
            await _context.SaveChangesAsync();
            List<Planet> planets = await _repo.GetPlanetsAsync();
            Assert.NotEmpty(planets);
            Assert.IsType<Planet>(planets.FirstOrDefault());
        }

        [Fact]
        public async Task Assert_CreateHabitantAsync()
        {
            Habitant habitant = new Habitant { Name = "Luke Skywalker", IsRebel = true };
            await _repo.AddHabitantAsync(habitant);
            Habitant habitantFromDb = await _context.Habitants.FirstOrDefaultAsync(h => h.Name == "Luke Skywalker");
            Assert.NotNull(habitantFromDb);
            Assert.Equal(habitant.Name, habitantFromDb.Name);
        }


        [Fact]
        public async Task Assert_FindHabitantAsync()
        {
            _context.Habitants.Add(new Habitant { Id = 3, Name = "Yoda2", IsRebel = true });
            await _context.SaveChangesAsync();
            Habitant habitant = await _repo.GetHabitantByIdAsync(3);
            Assert.NotNull(habitant);
            Assert.Equal("Yoda2", habitant.Name);
        }

        [Fact]
        public async Task Assert_GetRebelsAsync()
        {
            // Agrega un habitante rebelde de prueba
            _context.Habitants.Add(new Habitant { Id = 4, Name = "Leia Organa", IdSpecie = 1, IdPlanetOfOrigin = 1, IsRebel = true });
            await _context.SaveChangesAsync();

            // Obtiene los rebeldes a través del repositorio
            List<Habitant> rebels = (List<Habitant>)await _repo.GetRebelsAsync(); // Asegúrate de que GetRebelsAsync esté implementado en el repositorio

            // Verifica que la lista de rebeldes no esté vacía
            Assert.NotEmpty(rebels);

            // Verifica que el primer rebelde en la lista es realmente un rebelde
            Assert.True(rebels.FirstOrDefault().IsRebel);
        }
    }
}
