using FunTrophy.Api.IntegrationTests.Utils;
using FunTrophy.Infrastructure;
using System;

namespace FunTrophy.Api.IntegrationTests
{
    public abstract class ServiceTest<IService> : DbIntegrationTest
    {
        protected FunTrophyContext _dbContext;

        protected ServiceTest(FunTrophyDbIntegrationFixture fixture) : base(fixture)
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