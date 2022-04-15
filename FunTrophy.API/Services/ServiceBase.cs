namespace FunTrophy.API.Services
{
    public class ServiceBase
    {
        protected readonly FunTrophyContext _dbContext;

        public ServiceBase(FunTrophyContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}