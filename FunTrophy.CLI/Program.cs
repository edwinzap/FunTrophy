using FunTrophy.Fake;
using FunTrophy.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Hello, World!");

var connectionString = "Data Source=C:/Users/forge/source/repos/FunTrophy/FunTrophy.Infrastructure/FunTrophy.sqlite";
var services = new ServiceCollection();

services.AddDbContext<FunTrophyContext>(options =>
        options.UseSqlite(connectionString), ServiceLifetime.Transient)
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