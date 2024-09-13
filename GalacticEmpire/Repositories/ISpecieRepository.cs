using GalacticEmpire.Models.DTOs;
using GalacticEmpire.Models.Entities;

namespace GalacticEmpire.Repositories
{
    public interface ISpecieRepository : IRepository<SpecieDto>
    {
        Task<SpecieDto> GetSpecieByNameAsync(string name);
    }
}
