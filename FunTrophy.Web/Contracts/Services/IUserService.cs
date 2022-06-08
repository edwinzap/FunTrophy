using FunTrophy.Shared.Model;

namespace FunTrophy.Web.Contracts.Services
{
    public interface IUserService
    {
        Task Add(AddUserDto user);

        Task Update(int userId, UpdateUserDto user);

        Task Remove(int userId);

        Task<List<UserDto>> GetAll();

        Task ChangePassword(int userId, string password);
    }
}