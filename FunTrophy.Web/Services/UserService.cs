using FunTrophy.Shared.Model;
using FunTrophy.Web.Contracts.Services;

namespace FunTrophy.Web.Services
{
    public class UserService : ServiceBase, IUserService
    {
        public UserService(HttpClient httpClient) : base(httpClient, "User")
        {
        }

        public Task Add(AddUserDto user)
        {
            throw new NotImplementedException();
        }

        public Task ChangePassword(int userId, string password)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task Remove(int userId)
        {
            throw new NotImplementedException();
        }

        public Task Update(int userId, UpdateUserDto user)
        {
            throw new NotImplementedException();
        }
    }
}
