using GalacticEmpire.Data;
using GalacticEmpire.Models;
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

        public async Task<IEnumerable<Habitant>> GetAllHabitantsAsync()
        {
            return await _context.Habitants
                .ToListAsync();
        }

        public async Task<Habitant> GetHabitantByIdAsync(int id)
        {
            return await _context.Habitants
                .FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task AddHabitantAsync(Habitant habitant)
        {
            await _context.Habitants.AddAsync(habitant);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Habitant>> GetRebelsAsync()
        {
            return await _context.Habitants
                .Where(h => h.IsRebel)
                .ToListAsync();
        }

        public async Task<List<Planet>> GetPlanetsAsync()
        {
            return await _context.Planets.ToListAsync();
        }

        public async Task<List<Specie>> GetSpeciesAsync()
        {
            return await _context.Species.ToListAsync();
        }
    }
}
