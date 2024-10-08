using MediaXAPIs.Data.Models;
using Microsoft.EntityFrameworkCore;
using static NuGet.Packaging.PackagingConstants;

namespace MediaXAPIs.Data
{
    public class MediaXDBContext : DbContext
    {
        public MediaXDBContext(DbContextOptions<MediaXDBContext> options) : base(options)
        {
                
        }

        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<UserProduct> UserProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // One-to-many relationship: ProductDetail to ProductImages
            modelBuilder.Entity<ProductImage>()
                .HasOne(pi => pi.ProductDetail)
                .WithMany(pd => pd.ProductImages)
                .HasForeignKey(pi => pi.ProductId) // ProductId is the foreign key
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete if a ProductDetail is removed
        }

    }
}
