using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ShoppingDemo.App.Data.Entites;

namespace ShoppingDemo.EFCore
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
      
        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(string connString) : base(GetOptions(connString))
        {
        }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost,1433; Database=sample-shop-db;User Id=sa; Password=Test100$;");
            }
        }

        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }

        public DbSet<Item> Items { get; set; }       
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> orderItems { get; set; }
        public DbSet<ApplicationUser> User { get; set; }
        public DbSet<Customer> Customer { get; set; }

        public DbSet<BillingAddress> BillingAddresses { get; set; }
        public DbSet<ShippingAddress> ShippingAddresses { get; set; }
        public DbSet<PaymentCard> PaymentCards { get; set; }
        public DbSet<CardInformation> CardInformation { get; set; }


    }
}
