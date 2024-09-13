using GalacticEmpire.Models.DTOs;
using GalacticEmpire.Models.Entities;

namespace GalacticEmpire.Repositories
{
    public interface IPlanetRepository : IRepository<PlanetDTO>
    {
        Task<PlanetDTO> GetPlanetByNameAsync(string name);
        Task AddAsync(PlanetDTO planetDto);
    }
}
