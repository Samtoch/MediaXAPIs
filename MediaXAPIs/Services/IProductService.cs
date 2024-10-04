﻿using MediaXAPIs.Data.Models;

namespace MediaXAPIs.Services
{
    public interface IProductService
    {
        Task<List<ProductDetail>> GetProducts();
        Task<ProductDetail> GetProduct(int productId);
        Task<List<ProductDetailDto>> GetProductWithImages();
        Task<ProductDetailDto> GetProductWithImages(int productId);
        Task<ResObjects<bool>> CreateProduct(ProductDetail productDetail);
        Task<ResObjects<bool>> CreateProductWithImages(ProductDetailDto productDetailDto);
        Task<ResObjects<bool>> EditProduct(ProductDetail productDetail);
        Task<ProductAndImage> GetProductAndImage(int productId);
        Task<List<ProductDetailPlusImg>> GetProductsAndImages();
        Task<ResObjects<bool>> CreateOrderDetails(List<OrderDetail> order);
        Task<ResObjects<bool>> CreateOrder(Order order);
        Task<List<OrderDetail>> GetOrderDetails(string id);
        Task<List<OrderDetail>> GetOrderDetails();
        Task<List<Order>> GetOrders();
    }
}
