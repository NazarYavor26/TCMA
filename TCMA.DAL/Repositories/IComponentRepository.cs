using TCMA.DAL.Entities;

namespace TCMA.DAL.Repositories
{
    public interface IComponentRepository
    {
        Task<IEnumerable<Component>> GetAllAsync(string? searchComponent);

        Task<Component?> GetByIdAsync(int id);

        Task<Component?> GetByUniqueNumberAsync(string uniqueNumber);

        Task<Component> CreateAsync(Component component);

        Task<Component> UpdateAsync(int componentId, Component component);

        Task<bool> DeleteAsync(int id);
    }
}
