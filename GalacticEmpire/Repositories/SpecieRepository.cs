using GalacticEmpire.Data;
using GalacticEmpire.Models.DTOs;
using GalacticEmpire.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GalacticEmpire.Repositories
{
    public class SpecieRepository : ISpecieRepository
    {
        private readonly GalacticEmpireContext _context;

        public SpecieRepository(GalacticEmpireContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SpecieDto>> GetAllAsync()
        {
            return await _context.Species
                .Select(s => new SpecieDto
                {
                    Name = s.Name
                })
                .ToListAsync();
        }

        public async Task AddAsync(SpecieDto specieDto)
        {
            var specie = new Specie
            {
                Name = specieDto.Name
            };

            await _context.Species.AddAsync(specie);
            await _context.SaveChangesAsync();
        }

        public async Task<SpecieDto> GetSpecieByNameAsync(string name)
        {
            return await _context.Species
                .Where(s => s.Name == name)
                .Select(s => new SpecieDto
                {
                    Name = s.Name
                })
                .FirstOrDefaultAsync();
        }
    }
}
