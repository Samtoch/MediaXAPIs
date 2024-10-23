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
using MediaXAPIs.Services.User_;

namespace MediaXAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _userService.GetUsers();
            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var response = await _userService.GetUser(id);
            return Ok(response);
        }

        [HttpGet]
        [Route("Verify/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var response = await _userService.GetUser(email);
            return Ok(response);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult> CreateUser(UserCreateDTO user)
        {
            
            var response = await _userService.CreateUser(user);
            return StatusCode(response.ResCode, response);
        }

        [HttpPost]
        [Route("Update/{id}")]
        public async Task<IActionResult> UpdateProductImage(int id, UserCreateDTO user)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var response = await _userService.CreateUser(user);
            return StatusCode(response.ResCode, response);
        }
       
        [HttpPost]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteProductImage(int id)
        {
            var response = await _userService.GetUser(id);
            return Ok(response);
        }

        
    }
}
