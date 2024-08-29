using MediaXAPIs.Data;
using MediaXAPIs.Data.Models;

namespace MediaXAPIs.Services
{
    public class ImageService : IImageService
    {
        private readonly MediaXDBContext _dbContext;
        public async Task<List<ProductImage>> GetProductImage(string productId)
        {
            var image = new List<ProductImage>();
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
            return image;
        }
    }
}
