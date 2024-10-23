using MediaXAPIs.Data.Models;
using MediaXAPIs.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using NLog.Fluent;

namespace MediaXAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        private readonly IProductService _productService;
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        public ProductController(IProductService productService, IWebHostEnvironment env)
        {
            _productService = productService;
            _env = env;

        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDetailDto productDetail)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _productService.CreateProductWithImages(productDetail);
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

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddUserProduct([FromQuery] int userId, [FromQuery] int id)
        {
            if (userId <= 0 || id <= 0)
            {
                return BadRequest(new { success = false, message = "Invalid input: Username or Product Id is missing/invalid." });
            }
            var response = await _productService.AddUserProduct(userId, id);
            return StatusCode(response.ResCode, response);
        }

        [HttpGet]
        [Route("User/{userId}")]
        public async Task<IActionResult> GetUserProductAndImageById(int userId)
        {
            var response = await _productService.GetUserProducts(userId);
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

        [HttpGet]
        [Route("dir")]
        public async Task<IActionResult> GetRootDirectory()
        {

            string rootDirectory = Directory.GetCurrentDirectory();
            string hostDirectory = _env.ContentRootPath;
            log.Error($"The root directory id is {rootDirectory} and Host path is {hostDirectory}");
            string response = $"The root directory id is {rootDirectory} and Host path is {hostDirectory}" ;
            return Ok(response);
        }
    }
}
