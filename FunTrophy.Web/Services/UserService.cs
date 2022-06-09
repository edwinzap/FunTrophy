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
            var url = GetUrl();
            return PostAsync(url, user);
        }

        public Task ChangePassword(int userId, string password)
        {
            var url = GetUrl() + "/" + userId;
            return PostAsync(url, password);
        }

        public Task<List<UserDto>> GetAll()
        {
            var url = GetUrl();
            return GetAsync<List<UserDto>>(url);
        }

        public Task Remove(int userId)
        {
            var url = GetUrl() + "/" + userId;
            return DeleteAsync(url);
        }

        public Task Update(int userId, UpdateUserDto user)
        {
            var url = GetUrl() + "/" + userId;
            return UpdateAsync(url, user);
        }
    }
}
