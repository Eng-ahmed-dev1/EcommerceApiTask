using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ecomerec.DAL.Data
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<CartItem> CartItems { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // identity
            base.OnModelCreating(builder);

            var C = builder.Entity<Cart>();
            var CI = builder.Entity<CartItem>();
            var P = builder.Entity<Product>();


            P
            .HasMany(x => x.CartItems)
            .WithOne(c => c.Product)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

            C
            .HasMany(x => x.cartItems)
            .WithOne(c => c.Cart)
            .HasForeignKey(x => x.CartId)
            .OnDelete(DeleteBehavior.Cascade);

            C
            .HasOne(x => x.User)
            .WithOne(x => x.Cart)
            .HasForeignKey<Cart>(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        }
    }
}