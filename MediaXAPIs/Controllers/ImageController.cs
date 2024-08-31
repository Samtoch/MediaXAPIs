using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MediaXAPIs.Data;
using MediaXAPIs.Data.Models;
using MediaXAPIs.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MediaXAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imgService)
        {
            _imageService = imgService;
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _imageService.GetAllImage();
            return Ok(response);
        }

        [HttpGet]
        [Route("{productId}")]
        public async Task<IActionResult> GetImagesByProductId(string productId)
        {
            var response = await _imageService.GetProductImage(productId);
            return Ok(response);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult> CreateProductImage(ProductImageCreate productImage)
        {
            
            var response = await _imageService.CreateProductImage(productImage);
            return Ok(response);
            //return StatusCode(result.ResponseCode, response);
        }

        [HttpPost]
        [Route("Update/{id}")]
        public async Task<IActionResult> UpdateProductImage(int id, ProductImage productImage)
        {
            if (id != productImage.Id)
            {
                return BadRequest();
            }

            var response = await _imageService.UpdateProductImage(id, productImage);
            return Ok(response);
        }
       
        [HttpPost]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteProductImage(int id)
        {
            var response = await _imageService.DeleteProductImage(id);
            return Ok(response);
        }

        
    }
}
