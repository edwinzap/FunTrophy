using FunTrophy.Infrastructure;
using FunTrophy.Tests.Utils;
using System;

namespace FunTrophy.API.IntegrationTests
{
    public abstract class RepositoryTest<IService> : DbIntegrationTest
    {
        protected FunTrophyContext _dbContext;

        protected RepositoryTest(FunTrophyDbIntegrationFixture fixture) : base(fixture)
        {
        }

        protected abstract Func<FunTrophyContext, IService> CreateSut { get; }

        protected IService Sut
        {
            get
            {
                _dbContext = CreateDbContext();
                return CreateSut(_dbContext);
            }
        }

        public override void Dispose()
        {
            _dbContext.Dispose();
            base.Dispose();
        }
    }
}