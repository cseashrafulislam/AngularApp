using AngularApp1.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace AngularApp1.Server.DB
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets for the entities
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<OrderMst> OrderMsts { get; set; }
        public DbSet<OrderDtls> OrderDtls { get; set; }

        // Configure relationships using Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure many-to-one relationship between OrderDtls and OrderMst
            modelBuilder.Entity<OrderDtls>()
                .HasOne(od => od.OrderMst)
                .WithMany(o => o.OrderDtls)
                .HasForeignKey(od => od.OrderMstId)
                .OnDelete(DeleteBehavior.Cascade);  // Deletes OrderDtls when an OrderMst is deleted

            // Configure many-to-one relationship between OrderDtls and Product
            modelBuilder.Entity<OrderDtls>()
                .HasOne(od => od.Product)
                .WithMany()
                .HasForeignKey(od => od.ProductId)
                .OnDelete(DeleteBehavior.Restrict);  // Prevents deletion of Product if it's referenced in an Order

            // Configure the many-to-one relationship between OrderMst and Customer
            modelBuilder.Entity<OrderMst>()
                .HasOne(o => o.Customer)
                .WithMany()
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);  // Prevents deletion of Customer if it's referenced in an Order
        }
    }

}
