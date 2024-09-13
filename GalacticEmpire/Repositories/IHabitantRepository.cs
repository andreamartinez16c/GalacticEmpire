using GalacticEmpire.Models.DTOs;
using GalacticEmpire.Models.Entities;

namespace GalacticEmpire.Repositories
{
    public interface IHabitantRepository : IRepository<HabitantDto>
    {
        Task<HabitantDto> GetHabitantByNameAsync(string name);
        Task<IEnumerable<HabitantDto>> GetHabitantsByPlanetNameAsync(string planetName); // Habitants X Planet
        Task<IEnumerable<HabitantDto>> GetHabitantsBySpecieNameAsync(string specieName); // Habitants X Specie
        Task<IEnumerable<HabitantDto>> GetRebelsAsync();
    }
}
