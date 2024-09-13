using GalacticEmpire.Data;
using GalacticEmpire.Models.DTOs;
using GalacticEmpire.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GalacticEmpire.Repositories
{
    public class PlanetRepository : IPlanetRepository
    {
        private readonly GalacticEmpireContext _context;

        public PlanetRepository(GalacticEmpireContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PlanetDTO>> GetAllAsync()
        {
            return await _context.Planets
                .Select(p => new PlanetDTO
                {
                    Name = p.Name
                })
            .ToListAsync();
        }

        public async Task AddAsync(PlanetDTO planetDto)
        {
            var planet = new Planet
            {
                Name = planetDto.Name
            };

            await _context.Planets.AddAsync(planet);
            await _context.SaveChangesAsync();
        }

        public async Task<PlanetDTO> GetPlanetByNameAsync(string name)
        {
            return await _context.Planets
            .Where(p => p.Name == name)
                .Select(p => new PlanetDTO
                {
                    Name = p.Name
                })
                .FirstOrDefaultAsync();
        }
    }
}
