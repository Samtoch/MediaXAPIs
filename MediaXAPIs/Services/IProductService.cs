using MediaXAPIs.Data.Models;

namespace MediaXAPIs.Services
{
    public interface IProductService
    {
        Task<List<ProductDetail>> GetProducts();
        Task<ProductDetail> GetProduct(int productId);
        Task<ProductAndImage> GetProductAndImage(int productId);
        Task<List<ProductDetailPlusImg>> GetProductsAndImages();
        Task<ResObjects<bool>> CreateOrderDetails(List<OrderDetail> order);
        Task<ResObjects<bool>> CreateOrder(Order order);
        Task<List<OrderDetail>> GetOrderDetails(string id);
        Task<List<OrderDetail>> GetOrderDetails();
        Task<List<Order>> GetOrders();
    }
}
