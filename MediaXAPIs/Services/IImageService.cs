using MediaXAPIs.Data.Models;

namespace MediaXAPIs.Services
{
    public interface IImageService
    {
        Task<List<ProductImage>> GetAllImage();
        Task<List<ProductImage>> GetProductImage(string productId);
        Task<ResObjects<string>> CreateProductImage(ProductImageCreate productImage);
        Task<ResObjects<string>> UpdateProductImage(int id, ProductImage productImage);
        Task<ResObjects<string>> DeleteProductImage(int id);
    }
}
