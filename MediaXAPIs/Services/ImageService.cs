using MediaXAPIs.Data;
using MediaXAPIs.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MediaXAPIs.Services
{
    public class ImageService : IImageService
    {
        private readonly MediaXDBContext _dbContext;

        public ImageService(MediaXDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<ProductImage>> GetAllImage()
        {
            var image = new List<ProductImage>();
            try
            {
                image = await _dbContext.ProductImages.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
            return image;
        }

        public async Task<List<ProductImage>> GetProductImage(string productId)
        {
            var image = new List<ProductImage>();
            try
            {
                image = await _dbContext.ProductImages.Where(x => x.ProductId == productId).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return image;
        }

        public async Task<ResObjects<string>> CreateProductImage(ProductImageCreate productImage)
        {
            var image = new ProductImage() { 
                DelFlag = "N",
                ImageId = productImage.ImageId, 
                ProductId = productImage.ProductId, 
                ImageString = productImage.ImageString
            };
            var response = new ResObjects<string>();
            try
            {
                _dbContext.ProductImages.Add(image);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                  return new ResObjects<string> { Data = "Not Created", ResCode = 500, ResFlag = false, ResMsg = "Not Created" };
            }
            return new ResObjects<string> { Data = "Successfully Created", ResCode = 200, ResFlag = true, ResMsg = "Created"};
        }

        public async Task<ResObjects<string>> UpdateProductImage(int id, ProductImage productImage)
        {
            _dbContext.Entry(productImage).State = EntityState.Modified;
            var response = new ResObjects<string>();
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (!ProductImageExists(id))
                {
                    return new ResObjects<string> { Data = "Failed to update", ResCode = 404, ResFlag = false, ResMsg = "Not Found" };
                }
                else
                {
                    throw;
                }
            }
            return new ResObjects<string> { Data = "Successfully Updated", ResCode = 200, ResFlag = true, ResMsg = "Not Found" };
        }

        public async Task<ResObjects<string>> DeleteProductImage(int id)
        {
            var response = new ResObjects<string>();
            try
            {
                var productImage = await _dbContext.ProductImages.FindAsync(id);
                if (productImage == null)
                {
                    return new ResObjects<string> { Data = "Failed to Delete", ResCode = 404, ResFlag = false, ResMsg = "Not Found" };
                }
                _dbContext.ProductImages.Remove(productImage);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResObjects<string> { Data = "Failed Deleted", ResCode = 500, ResFlag = true, ResMsg = "Error Occured" };
                throw;
            }
            return new ResObjects<string> { Data = "Successfully Deleted", ResCode = 200, ResFlag = true, ResMsg = "Deleted" };
        }

        private bool ProductImageExists(int id)
        {
            return _dbContext.ProductImages.Any(e => e.Id == id);
        }
    }
}
