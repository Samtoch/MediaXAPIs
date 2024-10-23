using MediaXAPIs.Data.Models;

namespace MediaXAPIs.Services.User_
{
    public interface IUserService
    {
        Task<List<User>> GetUsers();
        Task<User> GetUser(int id);
        Task<User> GetUser(string email);
        Task<ResObjects<bool>> CreateUser(UserCreateDTO userdto);
    }
}
