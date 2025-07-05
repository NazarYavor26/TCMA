using TCMA.BLL.Models;

namespace TCMA.BLL.Services
{
    public interface IComponentService
    {
        Task<PagedResult<ComponentGetModel>> GetAllAsync(ComponentFilterModel filter);

        Task<ComponentGetModel> GetByIdAsync(int id);

        Task<ComponentGetModel> CreateAsync(ComponentCreateModel componentCreate);

        Task<ComponentGetModel> UpdateAsync(int componentId, ComponentUpdateModel componentUpdate);

        Task<ComponentGetModel> UpdateQuantityAsync(int componentId, QuantityUpdateModel quantityUpdate);

        Task<bool> DeleteAsync(int id);
    }
}
