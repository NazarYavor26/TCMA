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

        public async Task<IEnumerable<ComponentGetModel>> GetAllAsync(string? searchComponent)
        {
            var components = await _componentRepository.GetAllAsync(searchComponent);
            return components.Select(c => c.ToModel());
        }

        public async Task<ComponentGetModel> GetByIdAsync(int id)
        {
            var component = await _componentRepository.GetByIdAsync(id);
            return component.ToModel();
        }

        public async Task<ComponentGetModel> CreateAsync(ComponentSaveModel component)
        {
            var createdComponent = await _componentRepository.CreateAsync(component.ToEntity());
            return createdComponent.ToModel();
        }

        public async Task<ComponentGetModel> UpdateAsync(int componentId, ComponentSaveModel component)
        {
            var updatedComponent = await _componentRepository.UpdateAsync(componentId, component.ToEntity());
            return updatedComponent.ToModel();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _componentRepository.DeleteAsync(id);
        }
    }
}
