using GalacticEmpire.Models;

namespace GalacticEmpire.Repositories
{
    public interface IHabitantRepository
    {
        Task<IEnumerable<Habitant>> GetAllHabitantsAsync();
        Task<Habitant> GetHabitantByIdAsync(int id);
        Task AddHabitantAsync(Habitant habitant);
        Task<IEnumerable<Habitant>> GetRebelsAsync();
        Task<List<Planet>> GetPlanetsAsync();
        Task<List<Specie>> GetSpeciesAsync();
    }
}
