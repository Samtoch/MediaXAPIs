using MediaXAPIs.Data;
using MediaXAPIs.Data.Models;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace MediaXAPIs.Services.User_
{
    public class UserService : IUserService
    {
        private readonly MediaXDBContext _dbContext;
        private readonly IImageService _imageService;
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        public UserService(MediaXDBContext dbContext, IImageService imageService)
        {
            _dbContext = dbContext;
            _imageService = imageService;
        }

        public async Task<List<User>> GetUsers()
        {
            var users = new List<User>();
            try
            {
                users = await _dbContext.Users.Where(x => !x.DelFlag).ToListAsync();
            }
            catch (Exception ex)
            {
                log.Error("GetUsers " + ex);
            }
            return users;
        }

        public async Task<User> GetUser(int id)
        {
            var users = new User();
            try
            {
                users = await _dbContext.Users.FirstOrDefaultAsync(u => !u.DelFlag && u.Id == id );
            }
            catch (Exception ex)
            {
                log.Error("GetUser with id: " + id + ", error: " + ex);
            }
            return users;
        }

        public async Task<User> GetUser(string email)
        {
            var users = new User();
            try
            {
                users = await _dbContext.Users.FirstOrDefaultAsync(u => !u.DelFlag && u.Email == email);
            }
            catch (Exception ex)
            {
                log.Error("GetUser with email: " + email + ", error: " + ex);
            }
            return users;
        }


        public async Task<ResObjects<bool>> CreateUser(UserCreateDTO userdto)
        {

            var response = new ResObjects<bool>();
            var existingUser = await GetUser(userdto.Email);
            if (existingUser != null) return new ResObjects<bool> { Data = false, ResCode = 401, ResFlag = false, ResMsg = "User already exist" };
            try
            {
                int globalId = await GetlatestGlobalId();
                globalId++;

                var user = new User()
                {
                   Firstname = userdto.Firstname,
                   Lastname = userdto.Lastname,
                   Email = userdto.Email,
                   Phone = userdto.Phone,
                   Username = userdto.Email,
                   GlobalId = globalId
                };

                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync();

                response = new ResObjects<bool> { Data = true, ResCode = 201, ResMsg = "User created successfully" };
            }
            catch (Exception ex)
            {
                log.Error("CreateUser with user email: " + userdto.Email + ", error: " + ex);
                response = new ResObjects<bool> { Data = false, ResCode = 500, ResMsg = $"Error: {ex.Message}" };
            }

            return response;
        }

        public Task<int> GetlatestGlobalId()
        {
            int globalId = _dbContext.Users.Any() ? _dbContext.Users.Max(u => u.GlobalId) : 1001;
            return Task.FromResult(globalId);
        }

        public async Task<ResObjects<bool>> EditProduct(ProductDetail productDetail)
        {
            var response = new ResObjects<bool>();
            try
            {
                _dbContext.ProductDetails.Update(productDetail);
                await _dbContext.SaveChangesAsync();
                response = new ResObjects<bool> { Data = true, ResCode = 200, ResMsg = "Product updated successfully" };
            }
            catch (Exception ex)
            {
                response = new ResObjects<bool> { Data = false, ResCode = 500, ResMsg = $"Error: {ex.Message}" };
            }
            return response;
        }

        
    }
}
