using FunTrophy.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Xunit;

namespace FunTrophy.Tests.Utils
{
    [Collection(nameof(FunTrophyDbIntegrationCollection))]
    public abstract class DbIntegrationTest : IDisposable
    {
        private readonly FunTrophyDbIntegrationFixture _fixture;
        private readonly IDbContextTransaction _tran;

        protected DbIntegrationTest(FunTrophyDbIntegrationFixture fixture)
        {
            _fixture = fixture;
            _tran = fixture.CreateDbContext().Database.BeginTransaction();
        }

        protected Func<FunTrophyContext> CreateDbContext => () =>
          {
              var ctx = _fixture.CreateDbContext();
              ctx.Database.UseTransaction(_tran.GetDbTransaction());
              return ctx;
          };

        protected void Arrange(Action<FunTrophyContext> arrange)
        {
            using var ctx = CreateDbContext();
            arrange(ctx);
            ctx.SaveChanges();
        }

        public virtual void Dispose()
        {
            _tran.Rollback();
            _tran.Dispose();
        }
    }
}