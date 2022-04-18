using FunTrophy.Infrastructure;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using Xunit;

namespace FunTrophy.Api.IntegrationTests.Utils
{
    public class FunTrophyDbIntegrationFixture : IDisposable
    {
        private readonly SqlConnection _connection;
        private readonly FunTrophyContext _dbContext;

        public FunTrophyDbIntegrationFixture()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            _connection = new SqlConnection(config.GetConnectionString(nameof(FunTrophyContext)));

            _dbContext = CreateDbContext();
            _dbContext.Database.EnsureDeleted();
            //_dbContext.Database.EnsureCreated();
            _dbContext.Database.Migrate();
        }

        public Func<FunTrophyContext> CreateDbContext => () =>
             new FunTrophyContext(new DbContextOptionsBuilder<FunTrophyContext>()
                 .UseSqlServer(_connection)
                 .Options);

        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
            _connection.Dispose();
        }
    }

    [CollectionDefinition(nameof(FunTrophyDbIntegrationCollection))]
    public class FunTrophyDbIntegrationCollection : ICollectionFixture<FunTrophyDbIntegrationFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}