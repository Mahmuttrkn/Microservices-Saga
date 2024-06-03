using Microsoft.EntityFrameworkCore;

namespace OrderAPI
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
