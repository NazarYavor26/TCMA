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

        public async Task<IEnumerable<Component>> GetAllAsync(string? searchComponent)
        {
            IQueryable<Component> query = _db.Components;

            if (!string.IsNullOrWhiteSpace(searchComponent))
            {
                query = query.Where(c =>
                c.Name.Contains(searchComponent) ||
                c.UniqueNumber.Contains(searchComponent));
            }

            return await query.ToListAsync();
        }

        public async Task<Component?> GetByIdAsync(int id)
        {
            return await _db.Components
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
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
                .Where(c => c.Id == componentId)
                .FirstOrDefaultAsync();

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
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();

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
