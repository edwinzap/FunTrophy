using FunTrophy.API;
using FunTrophy.API.Contracts.Services;
using FunTrophy.Fake;
using FunTrophy.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Hello, World!");

//var connectionString = "Server=127.0.0.1,1433;Initial Catalog=FunTrophy;User ID=SA;Password=Admin123!;Integrated Security=False;MultipleActiveResultSets=False;TrustServerCertificate=True;Connection Timeout=30;";
var connectionString = "Server=(LocalDb)\\MSSQLLocalDB;Initial Catalog=funtrophy;Integrated Security=True;MultipleActiveResultSets=False;";
var services = new ServiceCollection();

//services
//    .AddServices()
//    .AddMappers()
//    .AddHelpers()
//    .AddRepositories()
//    .AddSignalR();

//services.AddLogging();
services.AddDbContext<FunTrophyContext>(options =>
        options.UseSqlServer(connectionString), ServiceLifetime.Transient)
    .AddTransient<FunTrophyInitializer>();

var serviceProvider = services
    .BuildServiceProvider();

var initializer = serviceProvider.GetService<FunTrophyInitializer>();

if (initializer is not null)
{
    Console.WriteLine("Seed data...");
    initializer.SeedData();
    await initializer.ApplySeeding();
    Console.WriteLine("Seeding done!");
}