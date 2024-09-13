namespace GalacticEmpire.Repositories
{
    public interface IRepository<TDto> where TDto : class
    {
        Task<IEnumerable<TDto>> GetAllAsync();
        Task AddAsync(TDto entityDto);
    }
}
