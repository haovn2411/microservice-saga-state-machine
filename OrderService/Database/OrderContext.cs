using Microsoft.EntityFrameworkCore;
using OrderService.Database;

namespace Order.Service.API.Database
{
    public class OrderContext : DbContext
    {
        public OrderContext()
        {

        }

        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderEntityState>().HasKey(e => e.CorrelationId);
        }

        public DbSet<OrderEntityState> orders { get; set; }

    }
}
