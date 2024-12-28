using MediaXAPIs.Data.Models;

namespace MediaXAPIs.Services
{
    public interface IProductService
    {
        Task<List<ProductDetail>> GetProducts();
        Task<List<ProductDetail>> AddedProducts();
        Task<List<ProductDetail>> AddedUserProducts(int userId);
        Task<List<ProductDetailDto>> GetUserProductWithImages(int userId);
        Task<List<ProductDetail>> AvailableProducts();
        Task<ProductDetail> GetProduct(int productId);
        Task<List<ProductDetailPlusImg>> GetProductsAndImages();
        Task<List<ProductDetailPlusImg>> GetAddedProductsAndImages();
        Task<List<ProductDetailDto>> GetProductWithImages();
        Task<ProductDetailDto> GetProductWithImages(int productId);
        Task<ResObjects<bool>> CreateProduct(ProductDetail productDetail);
        Task<ResObjects<bool>> CreateProductWithImages(ProductDetailDto productDetailDto);
        Task<ResObjects<bool>> AddUserProduct(int userId, int id);
        Task<ResObjects<bool>> RemoveUserProduct(int userId, int id);
        Task<List<UserProduct>> GetUserProducts(int userId);
        Task<ResObjects<bool>> EditProduct(ProductDetail productDetail);
        Task<ProductAndImage> GetProductAndImage(int productId);
        
        Task<ResObjects<bool>> CreateOrderDetails(List<OrderDetail> order);
        Task<ResObjects<bool>> CreateOrder(Order order);
        Task<List<OrderDetail>> GetOrderDetails(string id);
        Task<List<OrderDetail>> GetOrderDetails();
        Task<List<Order>> GetOrders();

        Task<List<UserAddedProduct>> GetUserAddedProducts(int id);
    }
}
