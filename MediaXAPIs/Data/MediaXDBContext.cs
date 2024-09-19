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
        public DbSet<Order> Orders { get; set; }
    }
}
