using FunTrophy.Shared.Model;

namespace FunTrophy.API.Contracts.Services
{
    public interface IUserService
    {
        Task ChangePassword(int userId, string password);

        Task Create(AddUserDto user);

        Task Remove(int userId);
    }
}
