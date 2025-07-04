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

            if (component == null)
            {
                throw new KeyNotFoundException($"Component with id {id} not found.");
            }

            return component.ToModel();
        }

        public async Task<ComponentGetModel> CreateAsync(ComponentSaveModel component)
        {
            var existingСomponent = await _componentRepository.GetByUniqueNumberAsync(component.UniqueNumber);

            if (existingСomponent != null)
            {
                throw new InvalidOperationException($"Component with UniqueNumber {component.UniqueNumber} already exists.");
            }

            var createdComponent = await _componentRepository.CreateAsync(component.ToEntity());
            return createdComponent.ToModel();
        }

        public async Task<ComponentGetModel> UpdateAsync(int componentId, ComponentSaveModel component)
        {
            var existingСomponent = await _componentRepository.GetByIdAsync(componentId);

            if (existingСomponent == null)
            {
                throw new KeyNotFoundException($"Component with id {componentId} not found.");
            }

            if (existingСomponent.UniqueNumber != component.UniqueNumber)
            {
                var duplicate = await _componentRepository.GetByUniqueNumberAsync(component.UniqueNumber);

                if (duplicate != null)
                {
                    throw new InvalidOperationException($"Component with UniqueNumber {component.UniqueNumber} already exists.");
                }
            }

            var updatedComponent = await _componentRepository.UpdateAsync(componentId, component.ToEntity());
            return updatedComponent.ToModel();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _componentRepository.DeleteAsync(id);
        }
    }
}
