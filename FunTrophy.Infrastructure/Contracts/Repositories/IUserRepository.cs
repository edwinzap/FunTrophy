using FunTrophy.Infrastructure.Model;

namespace FunTrophy.Infrastructure.Contracts.Repositories
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<User?> Get(string username, string password);
    }
}