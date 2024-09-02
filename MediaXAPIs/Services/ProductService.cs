using MediaXAPIs.Data;
using MediaXAPIs.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MediaXAPIs.Services
{
    public class ProductService : IProductService
    {
        private readonly MediaXDBContext _dbContext;
        private readonly IImageService _imageService;

        public ProductService(MediaXDBContext dbContext, IImageService imageService)
        {
            _dbContext = dbContext;
            _imageService = imageService;
        }
        public async Task<List<ProductDetail>> GetProducts()
        {
            var product = new List<ProductDetail>();
            try
            {
                product = await _dbContext.ProductDetails.Where(x => x.DelFlag == 'N').ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return product;
        }

        public async Task<ProductDetail> GetProduct(int productId)
        {
            var product = new ProductDetail();
            try
            {
                product = await _dbContext.ProductDetails.Where(x => x.DelFlag == 'N' && x.Id == productId).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return product;
        }

        public async Task<List<ProductDetailPlusImg>> GetProductsAndImages()
        {
            var product = new ProductAndImage();
            var images = new ProductImage();
            var prodAndMainImage = new ProductDetailPlusImg();
            var productsAndMainImage = new List<ProductDetailPlusImg>();
            try
            {
                var products = await GetProducts();
                foreach (var prod in products)
                {
                    var image = _imageService.GetProductImage(prod.Id).Result.Where(x => x.IsMain == true).FirstOrDefault();
                    if (image != null)
                    {
                        prodAndMainImage = new ProductDetailPlusImg()
                        {
                            Id = prod.Id,
                            Description = prod.Description,
                            ProductName = prod.ProductName,
                            Promoted = prod.Promoted,
                            Ratings = prod.Ratings,
                            DelFlag = prod.DelFlag,
                            Price = prod.Price,
                            ImageUrl = image.ImageId,
                        };
                        productsAndMainImage.Add(prodAndMainImage);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return productsAndMainImage;
        }
        public async Task<ProductAndImage> GetProductAndImage(int productId)
        {
            var product = new ProductAndImage();
            try
            {
                var prodDetails = await GetProduct(productId);
                var imageDetails = await _imageService.GetProductImage(productId);

                product = new ProductAndImage() { Product = prodDetails, Image = imageDetails };
            }
            catch (Exception)
            {
                throw;
            }
            return product;
        }


    }
}
