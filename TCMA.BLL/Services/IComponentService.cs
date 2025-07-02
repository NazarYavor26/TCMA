using TCMA.BLL.Models;

namespace TCMA.BLL.Services
{
    public interface IComponentService
    {
        Task<IEnumerable<ComponentModel>> GetAllAsync(string? searchComponent);

        Task<ComponentModel> GetByIdAsync(int id);
    }
}
