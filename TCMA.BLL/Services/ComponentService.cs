using TCMA.BLL.Models;
using TCMA.BLL.Utilities;
using TCMA.DAL.Repositories;

namespace TCMA.BLL.Services
{
    public class ComponentService : IComponentService
    {
        private readonly IComponentRepository _componentRepository;

        public ComponentService(IComponentRepository componentRepository)
        {
            _componentRepository = componentRepository;
        }

        public async Task<IEnumerable<ComponentModel>> GetAllAsync(string? searchComponent)
        {
            var components = await _componentRepository.GetAllAsync(searchComponent);
            return components.Select(c => c.ToModel());
        }
    }
}
