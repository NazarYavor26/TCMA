using Microsoft.EntityFrameworkCore;
using TCMA.DAL.DbContexts;
using TCMA.DAL.Entities;

namespace TCMA.DAL.Repositories
{
    public class ComponentRepository : IComponentRepository
    {
        private readonly AppDbContext _db;

        public ComponentRepository(AppDbContext db)
        {
            _db = db;
        }

        public IQueryable<Component> GetQueryable()
        {
            return _db.Components.AsNoTracking();
        }

        public async Task<Component?> GetByIdAsync(int id)
        {
            return await _db.Components
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Component?> GetByUniqueNumberAsync(string uniqueNumber)
        {
            return await _db.Components
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.UniqueNumber == uniqueNumber);
        }

        public async Task<Component> CreateAsync(Component component)
        {
            await _db.Components.AddAsync(component);
            await _db.SaveChangesAsync();
            return component;
        }

        public async Task<Component> UpdateAsync(int componentId, Component component)
        {
            var existingСomponent = await _db.Components
                .FirstOrDefaultAsync(c => c.Id == componentId);

            if (existingСomponent == null)
            {
                return null;
            }

            existingСomponent.Name = component.Name;
            existingСomponent.UniqueNumber = component.UniqueNumber;
            existingСomponent.CanAssignQuantity = component.CanAssignQuantity;
            existingСomponent.Quantity = component.Quantity;

            await _db.SaveChangesAsync();
            return existingСomponent;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingСomponent = await _db.Components
                .FirstOrDefaultAsync(c => c.Id == id);

            if (existingСomponent == null)
            {
                return false;
            }

            _db.Components.Remove(existingСomponent);
            await _db.SaveChangesAsync();

            return true;
        }
    }
}
