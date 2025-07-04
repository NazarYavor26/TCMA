using TCMA.BLL.Models;

namespace TCMA.BLL.Services
{
    public interface IComponentService
    {
        Task<PagedResult<ComponentGetModel>> GetAllAsync(ComponentFilterModel filter);

        Task<ComponentGetModel> GetByIdAsync(int id);

        Task<ComponentGetModel> CreateAsync(ComponentSaveModel component);

        Task<ComponentGetModel> UpdateAsync(int componentId, ComponentSaveModel component);

        Task<bool> DeleteAsync(int id);
    }
}
