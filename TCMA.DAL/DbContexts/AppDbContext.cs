using Microsoft.EntityFrameworkCore;
using TCMA.DAL.Entities;

namespace TCMA.DAL.DbContexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Component> Components { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Component>()
                .HasIndex(c => c.Name);

            modelBuilder.Entity<Component>()
                .HasIndex(c => c.UniqueNumber)
                .IsUnique();
        }
    }
}
