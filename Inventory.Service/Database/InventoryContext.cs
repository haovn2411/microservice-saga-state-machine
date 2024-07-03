using Inventory.Service.Database;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Service.API.Database
{
    public class InventoryContext : DbContext
    {
        IConfiguration configuration;
        public InventoryContext()
        {

        }

        public InventoryContext(DbContextOptions<InventoryContext> options) : base(options)
        {

        }

        public DbSet<InventoryEntity> inventories { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<InventoryEntity>().HasData(
                new InventoryEntity
                {
                    id = Guid.NewGuid().ToString("N"),
                    name = "Iphone",
                    price = 10,
                    unit = 5
                },
               new InventoryEntity
               {
                   id = Guid.NewGuid().ToString("N"),
                   name = "Samsung",
                   price = 10,
                   unit = 5
               },
               new InventoryEntity
               {
                   id = "6",
                   name = "Sony",
                   price = 10,
                   unit = 5
               }
                );
        }

    }
}
