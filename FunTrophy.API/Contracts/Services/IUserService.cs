using FunTrophy.Shared.Model;

namespace FunTrophy.API.Contracts.Services
{
    public interface IUserService
    {
        Task ChangePassword(int userId, string password);

        Task<int> Create(AddUserDto user);

        Task Update(int userId, UpdateUserDto user);

        Task Remove(int userId);

        Task<List<UserDto>> GetAll();
    }
}