using GalacticEmpire.Data;
using GalacticEmpire.Models.DTOs;
using GalacticEmpire.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GalacticEmpire.Repositories
{
    public class HabitantRepository : IHabitantRepository
    {
        private readonly GalacticEmpireContext _context;

        public HabitantRepository(GalacticEmpireContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HabitantDto>> GetAllAsync()
        {
            return await _context.Habitants
                .Join(_context.Species, h => h.IdSpecie, s => s.IdSpecie, (h, s) => new { h, s })
                .Join(_context.Planets, hs => hs.h.IdPlanetOfOrigin, p => p.IdPlanet, (hs, p) => new HabitantDto
                {
                    Name = hs.h.Name,
                    SpecieName = hs.s.Name,
                    PlanetName = p.Name,
                    IsRebel = hs.h.IsRebel
                })
                .ToListAsync();
        }

        public async Task AddAsync(HabitantDto habitantDto)
        {
            var habitant = new Habitant
            {
                Name = habitantDto.Name,
                IdSpecie = _context.Species.FirstOrDefault(s => s.Name == habitantDto.SpecieName)?.IdSpecie ?? 0,
                IdPlanetOfOrigin = _context.Planets.FirstOrDefault(p => p.Name == habitantDto.PlanetName)?.IdPlanet ?? 0,
                IsRebel = habitantDto.IsRebel
            };

            await _context.Habitants.AddAsync(habitant);
            await _context.SaveChangesAsync();
        }

        public async Task<HabitantDto> GetHabitantByNameAsync(string name)
        {
            return await _context.Habitants
                .Where(h => h.Name == name)
                .Join(_context.Species, h => h.IdSpecie, s => s.IdSpecie, (h, s) => new { h, s })
                .Join(_context.Planets, hs => hs.h.IdPlanetOfOrigin, p => p.IdPlanet, (hs, p) => new HabitantDto
                {
                    Name = hs.h.Name,
                    SpecieName = hs.s.Name,
                    PlanetName = p.Name,
                    IsRebel = hs.h.IsRebel
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<HabitantDto>> GetHabitantsByPlanetNameAsync(string planetName)
        {
            return await _context.Habitants
                .Join(_context.Species, h => h.IdSpecie, s => s.IdSpecie, (h, s) => new { h, s })
                .Join(_context.Planets, hs => hs.h.IdPlanetOfOrigin, p => p.IdPlanet, (hs, p) => new { hs.h, hs.s, p })
                .Where(result => result.p.Name == planetName) 
                .Select(result => new HabitantDto
                {
                    Name = result.h.Name,
                    SpecieName = result.s.Name,
                    PlanetName = result.p.Name,
                    IsRebel = result.h.IsRebel
                })
                .ToListAsync();
        }


        public async Task<IEnumerable<HabitantDto>> GetHabitantsBySpecieNameAsync(string specieName)
        {
            return await _context.Habitants
                .Join(_context.Species, h => h.IdSpecie, s => s.IdSpecie, (h, s) => new { h, s }) 
                .Where(hs => hs.s.Name == specieName) 
                .Join(_context.Planets, hs => hs.h.IdPlanetOfOrigin, p => p.IdPlanet, (hs, p) => new HabitantDto
                {
                    Name = hs.h.Name,
                    SpecieName = hs.s.Name,
                    PlanetName = p.Name,
                    IsRebel = hs.h.IsRebel
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<HabitantDto>> GetRebelsAsync()
        {
            return await _context.Habitants
                .Where(h => h.IsRebel)
                .Join(_context.Species, h => h.IdSpecie, s => s.IdSpecie, (h, s) => new { h, s })
                .Join(_context.Planets, hs => hs.h.IdPlanetOfOrigin, p => p.IdPlanet, (hs, p) => new HabitantDto
                {
                    Name = hs.h.Name,
                    SpecieName = hs.s.Name,
                    PlanetName = p.Name,
                    IsRebel = hs.h.IsRebel
                })
                .ToListAsync();
        }
    }

}
