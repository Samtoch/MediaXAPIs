using MediaXAPIs.Data.Models;

namespace MediaXAPIs.Services
{
    public interface IImageService
    {
        Task<List<ProductImage>> GetProductImage(string productId);
    }
}
