using TCMA.BLL.Models;

namespace TCMA.BLL.Services
{
    public interface IComponentService
    {
        Task<IEnumerable<ComponentGetModel>> GetAllAsync(string? searchComponent);

        Task<ComponentGetModel> GetByIdAsync(int id);

        Task<ComponentGetModel> CreateAsync(ComponentSaveModel component);

        Task<ComponentGetModel> UpdateAsync(int componentId, ComponentSaveModel component);

        Task<bool> DeleteAsync(int id);
    }
}
