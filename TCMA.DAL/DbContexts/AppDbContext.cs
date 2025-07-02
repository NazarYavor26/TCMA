using Microsoft.EntityFrameworkCore;
using TCMA.DAL.Entities;

namespace TCMA.DAL.DbContexts
{
    internal class AppDbContext : DbContext
    {
        public DbSet<Component> Components { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        { }
    }
}
