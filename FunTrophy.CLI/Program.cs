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


Console.WriteLine("Choose an option:\n1) Create empty database \n2) Seed with fake data");
var selectedOption = Console.ReadLine();

if (initializer is not null && initializer is FunTrophyInitializer && selectedOption != null)
{
    if (selectedOption == "1")
    {
        Console.WriteLine("Create database");
        await initializer.CreateDatabase();
    }
        
    if (selectedOption == "2")
    {
        Console.WriteLine("Create database");
        await initializer.CreateDatabase();
        Console.WriteLine("Seeding data...");
        initializer.SeedData();
        await initializer.ApplySeeding();
        Console.WriteLine("Seeding done!");
    }
}