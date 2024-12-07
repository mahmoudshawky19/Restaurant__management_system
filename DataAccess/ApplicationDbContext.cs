using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
               : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityUser>()
                .Property(u => u.EmailConfirmed)
                .HasDefaultValue(true);  
        }

        public DbSet<Customer> customers { get; set; }
        public DbSet<SubmitReview> submits { get; set; }
        public DbSet<OrderTable> orderTables { get; set; }
        public DbSet<Cart> carts { get; set; }
        public DbSet<OrderMenuItem> orderMenuItems { get; set; }
        public DbSet<Supplier> suppliers { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<AssignedOrders> assignedOrders { get; set; }
        public DbSet<Employee> employees { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<MenuItem> menuItems { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<RestaurantTable> tables { get; set; }
        public DbSet<Reservation> reservations { get; set; }


    }
}
