using FunTrophy.Fake;
using FunTrophy.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Hello, World!");

var serviceProvider = new ServiceCollection()
    .AddDbContext<FunTrophyContext>(options =>
    options.UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;Initial Catalog=funtrophy;Integrated Security=True;MultipleActiveResultSets=False;"))
    .AddTransient<FunTrophyInitializer>()
    .BuildServiceProvider();

var initializer = serviceProvider.GetService<FunTrophyInitializer>();

if (initializer is not null)
{
    Console.WriteLine("Seed data...");
    initializer.SeedData();
    await initializer.ApplySeeding();
    Console.WriteLine("Seeding done!");
}