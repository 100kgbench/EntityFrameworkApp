using EntityFrameworkApp;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "EntityFrameworkAppAPI",
        Version = "v1"
    });
});

builder.Services.AddDbContext<AppDbContext>(options =>
   options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Pøidání DbContextu pro SQLite
//builder.Services.AddDbContext<AppDbContext>(options =>
//  options.UseSqlite("Data Source=EntityFrameworkApp.db"));

// Registrace tøíd pro Dependency Injection
builder.Services.AddScoped<Methods>();

var app = builder.Build();

// Konfigurace HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "EntityFrameworkAppAPI v1");
    }); 
}

app.MapControllers();
app.Run();
