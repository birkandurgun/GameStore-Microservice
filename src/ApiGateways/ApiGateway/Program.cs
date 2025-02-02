using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("Configurations/ocelot.json", optional: false, reloadOnChange: true);

// Add services to the container.

builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

await app.UseOcelot();

app.Run();
