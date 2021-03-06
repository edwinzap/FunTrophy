using Blazored.LocalStorage;
using FunTrophy.Web;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<AppState>();
builder.Services
    .AddServices(builder.Configuration)
    .AddHelpers()
    .AddAuthentication(builder.Configuration)
    .AddSettings(builder.Configuration);

builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();