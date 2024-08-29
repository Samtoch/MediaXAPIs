using MediaXAPIs.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MediaXAPIs.Data
{
    public class MediaXDBContext : DbContext
    {
        public MediaXDBContext(DbContextOptions<MediaXDBContext> options) : base(options)
        {
                
        }

        public DbSet<ProductImage> ProductImages { get; set; }
    }
}
