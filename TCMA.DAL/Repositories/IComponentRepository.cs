using TCMA.DAL.Entities;

namespace TCMA.DAL.Repositories
{
    public interface IComponentRepository
    {
        Task<IEnumerable<Component>> GetAllAsync(string? searchComponent);
    }
}
