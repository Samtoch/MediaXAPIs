using MediaXAPIs.Data;
using MediaXAPIs.Data.Models;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace MediaXAPIs.Services
{
    public class ProductService : IProductService
    {
        private readonly MediaXDBContext _dbContext;
        private readonly IImageService _imageService;
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

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
            catch (Exception ex)
            {
                log.Error("GetProduct " + ex);
            }
            return product;
        }

        public async Task<List<ProductDetail>> AvailableProducts()
        {
            var product = new List<ProductDetail>();
            try
            {
                product = await _dbContext.ProductDetails
                    .Where(pd => pd.DelFlag == 'N' && !_dbContext.UserProducts.Any(up => up.ProductId == pd.Id && !up.DelFlag)).ToListAsync();
            }
            catch (Exception ex)
            {
                log.Error("GetProduct " + ex);
            }
            return product;
        }

        public async Task<List<ProductDetail>> AddedProducts()
        {
            var product = new List<ProductDetail>();
            try
            {
                product = await _dbContext.ProductDetails
                    .Where(pd => pd.DelFlag == 'N' && _dbContext.UserProducts.Any(up => up.ProductId == pd.Id && !up.DelFlag)).ToListAsync();
            }
            catch (Exception ex)
            {
                log.Error("GetProduct " + ex);
            }
            return product;
        }

        public async Task<List<ProductDetail>> AddedUserProducts(int userId)
        {
            var product = new List<ProductDetail>();
            try
            {
                product = await _dbContext.ProductDetails
                    .Where(pd => pd.DelFlag == 'N' && _dbContext.UserProducts.Any(up => up.ProductId == pd.Id && up.UserId == userId && !up.DelFlag)).ToListAsync();
            }
            catch (Exception ex)
            {
                log.Error("GetProduct " + ex);
            }
            return product;
        }

        //public async Task<List<ProductDetail>> AddedUserProducts(int userId)
        //{
        //    var product = new List<ProductDetail>();
        //    try
        //    {
        //        product = await _dbContext.ProductDetails.Where(pd => pd.DelFlag == 'N' && _dbContext.UserProducts.Any(up => up.ProductId == pd.Id && up.UserId == userId)).ToListAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error("GetProduct " + ex);
        //    }
        //    return product;
        //}

        public async Task<ProductDetail> GetProduct(int productId)
        {
            var productDetail = new ProductDetail();
            try
            {
                productDetail = await _dbContext.ProductDetails.FirstOrDefaultAsync(p => p.Id == productId && p.DelFlag == 'N');

                if (productDetail == null) return null;
            }
            catch (Exception ex)
            {
                log.Error("GetProduct with id: " + productId + ", error: " + ex);
            }
            return productDetail;
        }

        public async Task<List<ProductDetailDto>> GetProductWithImages()
        {
            var products = new List<ProductDetailDto>();
            //var productDetail = await _dbContext.ProductDetails.Include(i => i.ProductImages).Where(p => p.DelFlag == 'N' && !_dbContext.UserProducts.Any(u => u.ProductId == p.Id)).ToListAsync();
            var productDetail = await _dbContext.ProductDetails.Include(i => i.ProductImages).Where(p => p.DelFlag == 'N').ToListAsync();

            if (productDetail == null) return null;

            // Map the ProductDetail and ProductImages to DTO

            foreach (var item in productDetail)
            {
                var product = new ProductDetailDto();
                product.Id = item.Id;
                product.ProductName = item.ProductName;
                product.Description = item.Description;
                product.Ratings = item.Ratings;
                product.Promoted = item.Promoted;
                product.Price = item.Price;
                product.ProductImages = item.ProductImages.Select(img => new ProductImageDto
                {
                    ImageId = img.ImageId,
                    ImageString = img.ImageString,
                    IsMain = img.IsMain,
                }).ToList();

                products.Add(product);
            }

            return products;
        }

        public async Task<List<ProductDetailDto>> GetUserProductWithImages(int userId)
        {
            var products = new List<ProductDetailDto>();
            var productDetail = await _dbContext.ProductDetails.Include(i => i.ProductImages)
                .Where(p => p.DelFlag == 'N' && _dbContext.UserProducts.Any(u => u.ProductId == p.Id && u.UserId == userId && !u.DelFlag)).ToListAsync();

            if (productDetail == null) return null;

            // Map the ProductDetail and ProductImages to DTO

            foreach (var item in productDetail)
            {
                var product = new ProductDetailDto();
                product.Id = item.Id;
                product.ProductName = item.ProductName;
                product.Description = item.Description;
                product.Ratings = item.Ratings;
                product.Promoted = item.Promoted;
                product.Price = item.Price;
                product.ProductImages = item.ProductImages.Select(img => new ProductImageDto
                {
                    ImageId = img.ImageId,
                    ImageString = img.ImageString,
                    IsMain = img.IsMain,
                }).ToList();

                products.Add(product);
            }

            return products;
        }

        public async Task<ProductDetailDto> GetProductWithImages(int productId)
        {
            var productDetail = await _dbContext.ProductDetails
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.Id == productId && p.DelFlag == 'N');

            if (productDetail == null) return null;

            // Map the ProductDetail and ProductImages to DTO
            return new ProductDetailDto
            {
                Id = productDetail.Id,
                ProductName = productDetail.ProductName,
                Description = productDetail.Description,
                Ratings = productDetail.Ratings,
                Price = productDetail.Price,
                Promoted = productDetail.Promoted,
                ProductImages = productDetail.ProductImages.Select(img => new ProductImageDto
                {
                    ImageId = img.ImageId,
                    ImageString = img.ImageString,
                    IsMain = img.IsMain,
                }).ToList()
            };
        }

        public async Task<ResObjects<bool>> CreateProductWithImages(ProductDetailDto productDetailDto)
        {
            var response = new ResObjects<bool>();

            try
            {
                var productDetail = new ProductDetail()
                {
                    ProductName = productDetailDto.ProductName,
                    Description = productDetailDto.Description,
                    Promoted = productDetailDto.Promoted,
                    Ratings = productDetailDto.Ratings,
                    Price = productDetailDto.Price,
                    ProductImages = productDetailDto.ProductImages.Select(img => new ProductImage { 
                        ImageId = img.ImageId,
                        ImageString = img.ImageString,
                        IsMain= img.IsMain
                    }).ToList()
                };

                _dbContext.ProductDetails.Add(productDetail);
                await _dbContext.SaveChangesAsync(); 

                response = new ResObjects<bool> { Data = true, ResCode = 201, ResMsg = "Product and images created successfully" };
            }
            catch (Exception ex)
            {
                response = new ResObjects<bool> { Data = false, ResCode = 500, ResMsg = $"Error: {ex.Message}" };
            }

            return response;
        }


        public async Task<ResObjects<bool>> CreateProduct(ProductDetail productDetail)
        {
            var response = new ResObjects<bool>();

            try
            {
                // Ensure the child ProductImages reference the correct ProductId
                if (productDetail.ProductImages != null && productDetail.ProductImages.Count > 0)
                {
                    foreach (var image in productDetail.ProductImages)
                    {
                        image.ProductId = productDetail.Id; // Assign ProductId to each image
                    }
                }

                // Add the ProductDetail along with ProductImages
                _dbContext.ProductDetails.Add(productDetail);
                await _dbContext.SaveChangesAsync();

                response = new ResObjects<bool> { Data = true, ResCode = 201, ResFlag = true, ResMsg = "Product with images created successfully" };
            }
            catch (Exception ex)
            {
                response = new ResObjects<bool> { Data = false, ResCode = 500, ResFlag = false, ResMsg = $"Error: {ex.Message}" };
            }

            return response;
        }


        public async Task<ResObjects<bool>> AddUserProduct(int userId, int id)
        {
            var response = new ResObjects<bool>();
            var product = new UserProduct() { ProductId = id, UserId = userId };
            try
            {
                _dbContext.UserProducts.Add(product);
                await _dbContext.SaveChangesAsync();

                response = new ResObjects<bool> { Data = true, ResCode = 201, ResFlag = true, ResMsg = "Product added successfully" };
            }
            catch (Exception ex)
            {
                response = new ResObjects<bool> { Data = false, ResCode = 500, ResFlag = false, ResMsg = $"Error: {ex.Message}" };
            }

            return response;
        }

        public async Task<ResObjects<bool>> RemoveUserProduct(int userId, int id)
        {
            var response = new ResObjects<bool>();
            var product = new UserProduct() { ProductId = id, UserId = userId };
            try
            {
                var userProd = await _dbContext.UserProducts.Where(x => x.UserId == userId && x.ProductId == id && !x.DelFlag).FirstOrDefaultAsync();
                if (userProd != null)
                {
                    userProd.DelFlag = true;

                    _dbContext.UserProducts.Entry(userProd).State = EntityState.Modified;
                    await _dbContext.SaveChangesAsync();

                    response = new ResObjects<bool> { Data = true, ResCode = 201, ResFlag = true, ResMsg = "Product Removed successfully" };
                    return response;
                }
                response = new ResObjects<bool> { Data = false, ResCode = 400, ResFlag = false, ResMsg = "Product Was Not Found" };
            }
            catch (Exception ex)
            {
                response = new ResObjects<bool> { Data = false, ResCode = 500, ResFlag = false, ResMsg = $"Error: {ex.Message}" };
            }

            return response;
        }

        public async Task<List<UserProduct>> GetUserProducts(int userId)
        {
            var product = new List<UserProduct>();
            try
            {
                product = await _dbContext.UserProducts.Where(x => x.DelFlag == false && x.UserId == userId).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return product;
        }

        public async Task<UserProduct> GetUserProduct(int id)
        {
            var product = new UserProduct();
            try
            {
                product = await _dbContext.UserProducts.Where(x => x.DelFlag == false && x.ProductId == id).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return product;
        }

        public async Task<ResObjects<bool>> EditProduct(ProductDetail productDetail)
        {
            var response = new ResObjects<bool>();
            try
            {
                _dbContext.ProductDetails.Update(productDetail);
                await _dbContext.SaveChangesAsync();
                response = new ResObjects<bool> { Data = true, ResCode = 200, ResFlag = true, ResMsg = "Product updated successfully" };
            }
            catch (Exception ex)
            {
                response = new ResObjects<bool> { Data = false, ResCode = 500, ResMsg = $"Error: {ex.Message}" };
            }
            return response;
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
                    var userProduct = await GetUserProduct(prod.Id);
                    if (userProduct != null)
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

                                Discount = userProduct.Discount,
                                GlobalId = userProduct.UserId
                            };
                            productsAndMainImage.Add(prodAndMainImage);
                        } 
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return productsAndMainImage;
        }

        public async Task<List<ProductDetailPlusImg>> GetAddedProductsAndImages()
        {
            var product = new ProductAndImage();
            var images = new ProductImage();
            var prodAndMainImage = new ProductDetailPlusImg();
            var productsAndMainImage = new List<ProductDetailPlusImg>();
            try
            {
                var products = await AddedProducts();
                foreach (var prod in products)
                {
                    var userProduct = await GetUserProduct(prod.Id);
                    if (userProduct != null)
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

                                Discount = userProduct.Discount,
                                GlobalId = userProduct.UserId
                            };
                            productsAndMainImage.Add(prodAndMainImage);
                        }
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

        public async Task<List<Order>> GetOrders()
        {
            var orders = new List<Order>();
            try
            {
                orders = await _dbContext.Orders.Where(x => x.DelFlag == false).ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
            return orders;
        }

        public async Task<ResObjects<bool>> CreateOrder(Order order)
        {
            var response = new ResObjects<bool>();
            bool resp = false;
            try
            {
                _dbContext.Orders.Add(order);
                await _dbContext.SaveChangesAsync();
                resp = true;
            }
            catch (Exception ex)
            {
                resp = false;
                response = new ResObjects<bool>{ Data = resp, ResCode = 500, ResFlag = resp, ResMsg = $"Failed: {ex.Message}" };
                throw;
            }
            response = new ResObjects<bool> { Data = resp, ResCode = 200, ResFlag = resp, ResMsg = "Successful" };
            return response;
        }

        public async Task<List<OrderDetail>> GetOrderDetails(string id)
        {
            var orders = new List<OrderDetail>();
            try
            {
                orders = await _dbContext.OrderDetails.Where(x => x.OrderReference == id && x.DelFlag == false).ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
            return orders;
        }

        public async Task<List<OrderDetail>> GetOrderDetails()
        {
            var orders = new List<OrderDetail>();
            try
            {
                orders = await _dbContext.OrderDetails.Where(x => x.DelFlag == false).ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
            return orders;
        }

        public async Task<ResObjects<bool>> CreateOrderDetails(List<OrderDetail> order)
        {
            var orderDetail = new List<OrderDetail>();
            var response = new ResObjects<bool>();
            bool resp = false;
            try
            {
                foreach (var item in order) {
                    var ord = new OrderDetail();
                    ord.Price = item.Price;
                    ord.Quantity = item.Quantity;
                    ord.ProductId = item.ProductId;
                    ord.OrderDate = item.OrderDate;
                    ord.ProductName = item.ProductName;
                    ord.OrderReference = item.OrderReference;
                    ord.TotalPrice = item.Price * item.Quantity;
                    orderDetail.Add(ord);
                }
                
                await _dbContext.OrderDetails.AddRangeAsync(orderDetail);
                await _dbContext.SaveChangesAsync();
                resp = true;
            }
            catch (Exception ex)
            {
                resp = false;
                response = new ResObjects<bool> { Data = resp, ResCode = 500, ResFlag = resp, ResMsg = $"Failed: {ex.Message}" };
                throw;
            }
            response = new ResObjects<bool> { Data = resp, ResCode = 200, ResFlag = resp, ResMsg = "Successful" };
            return response;
        }

        public async Task<List<UserAddedProduct>> GetUserAddedProducts(int id)
        {
            var product = new UserAddedProduct();

            var query = from u in _dbContext.Users
                        join up in _dbContext.UserProducts
                            on u.Id equals up.UserId into userProducts // Perform left join
                        from up in userProducts.DefaultIfEmpty() // Ensure left join behavior
                        where u.Id == id
                        select new UserAddedProduct
                        {
                            Id = u.Id,
                            GlobalId = u.GlobalId,
                            Username = u.Username,
                            Firstname = u.Firstname,
                            ProductId = up != null ? up.ProductId : (int?)null,
                            Discount = up != null ? up.Discount : (decimal?)null
                        };

            var result = query.ToList();


            return result;
            
        }

    }
}
