using AngularApp1.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace AngularApp1.Server.DB
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customer { get; set; }
    }
}
