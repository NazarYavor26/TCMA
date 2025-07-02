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
    }
}
