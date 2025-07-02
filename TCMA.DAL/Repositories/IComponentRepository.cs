using TCMA.DAL.Entities;

namespace TCMA.DAL.Repositories
{
    internal interface IComponentRepository
    {
        Task<IEnumerable<Component>> GetAllAsync(string? searchComponent);
    }
}
