using MediaXAPIs.Data.Models;
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

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDetail productDetail)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _productService.CreateProduct(productDetail);
            return StatusCode(response.ResCode, response);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> EditProduct(int id, [FromBody] ProductDetail productDetail)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != productDetail.Id)
                return BadRequest("Product ID mismatch");

            var response = await _productService.EditProduct(productDetail);
            return StatusCode(response.ResCode, response);
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
        [Route("New/All")]
        public async Task<IActionResult> GetAllProductnImages()
        {
            var response = await _productService.GetProductWithImages();
            return Ok(response);
        }

        [HttpGet]
        [Route("New/{id}")]
        public async Task<IActionResult> GetProductnImagesById(int id)
        {
            var response = await _productService.GetProductWithImages(id);
            return Ok(response);
        }

        [HttpGet]
        [Route("Details/{id}")]
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

        [HttpGet]
        [Route("Orders/All")]
        public async Task<IActionResult> GetAllOrders()
        {
            var response = await _productService.GetOrders();
            return Ok(response);
        }

        [HttpPost]
        [Route("Order")]
        public async Task<IActionResult> SubmitOrder([FromBody] Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _productService.CreateOrder(order);
            return StatusCode(response.ResCode, response);
        }

        [HttpGet]
        [Route("OrderDetails/All")]
        public async Task<IActionResult> GetAllOrderDetails()
        {
            var response = await _productService.GetOrderDetails();
            return Ok(response);
        }

        [HttpGet]
        [Route("OrderDetails/{id}")]
        public async Task<IActionResult> GetAllOrderDetails(string id)
        {
            var response = await _productService.GetOrderDetails(id);
            return Ok(response);
        }

        [HttpPost]
        [Route("OrderDetails")]
        public async Task<IActionResult> SubmitOrderDetails([FromBody] List<OrderDetail> order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _productService.CreateOrderDetails(order);
            return StatusCode(response.ResCode, response);
        }
    }
}
