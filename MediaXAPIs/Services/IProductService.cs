using MediaXAPIs.Data.Models;

namespace MediaXAPIs.Services
{
    public interface IProductService
    {
        Task<List<ProductDetail>> GetProducts();
        Task<ProductDetail> GetProduct(int productId);
        Task<ProductAndImage> GetProductAndImage(int productId);
        Task<List<ProductDetailPlusImg>> GetProductsAndImages();
        Task<ResObjects<bool>> CreateOrderDetails(OrderDetail order);
        Task<ResObjects<bool>> CreateOrder(Order order);
    }
}
