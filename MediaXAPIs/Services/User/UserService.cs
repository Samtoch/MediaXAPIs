using MediaXAPIs.Data;
using MediaXAPIs.Data.Models;
using MediaXAPIs.Services.Email;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace MediaXAPIs.Services.User_
{
    public class UserService : IUserService
    {
        private readonly MediaXDBContext _dbContext;
        private readonly IEmailService _emailService;
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        public UserService(MediaXDBContext dbContext, IEmailService emailService)
        {
            _dbContext = dbContext;
            _emailService = emailService;
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
            DateTime now = DateTime.Now;

            //await _emailService.SendEmailAsync(userdto.Email, "TESTING..." + now, "Test body");

            var emailRequest = new EmailRequest() { ToCC = userdto.Email, Firstname = userdto.Firstname, ToEmail = userdto.Email, isBodyHtml = true, Subject = "TESTING SUBJECT..." + now };
            await _emailService.SignUpNotification(emailRequest);
            //var emailResp = await _emailService.SendEmail(emailRequest);

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
                   Password = userdto.Password,
                   Role = userdto.Role,
                   GlobalId = globalId
                };

                await _emailService.SendEmailAsync(userdto.Email, "TESTING..." + now, "Test body");

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
