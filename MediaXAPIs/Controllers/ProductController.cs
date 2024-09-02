using MediaXAPIs.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediaXAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _productService.GetProducts();
            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var response = await _productService.GetProduct(id);
            return Ok(response);
        }

        [HttpGet]
        [Route("Details{id}")]
        public async Task<IActionResult> GetProductAndImageById(int id)
        {
            var response = await _productService.GetProductAndImage(id);
            return Ok(response);
        }

        [HttpGet]
        [Route("Details/All")]
        public async Task<IActionResult> GetAllProductsAndImages()
        {
            var response = await _productService.GetProductsAndImages();
            return Ok(response);
        }
    }
}
