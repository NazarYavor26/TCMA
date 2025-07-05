using Microsoft.EntityFrameworkCore;
using TCMA.DAL.Entities;

namespace TCMA.DAL.DbContexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Component> Components { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Component>()
                .HasIndex(c => c.Name);

            modelBuilder.Entity<Component>()
                .HasIndex(c => c.UniqueNumber)
                .IsUnique();

            modelBuilder.Entity<Component>()
                .ToTable(t => t.HasCheckConstraint(
                    "CK_Component_Quantity_Valid",
                    "([CanAssignQuantity] = 1 AND ([Quantity] IS NULL OR [Quantity] >= 0)) OR ([CanAssignQuantity] = 0 AND [Quantity] IS NULL)"
                ));
        }
    }
}
