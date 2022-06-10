using FunTrophy.Infrastructure.Contracts.Repositories;
using FunTrophy.Infrastructure.Model;
using Microsoft.EntityFrameworkCore;

namespace FunTrophy.Infrastructure.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(FunTrophyContext dbContext) : base(dbContext)
        {
        }

        public Task<User?> Get(string username, string password)
        {
            return _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == username && x.Password == password);
        }
    }
}