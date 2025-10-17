using APICatalog.Data;
using APICatalog.Repositories;
using APICatalog.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddControllersWithViews()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configura o DbContext com InMemory ou MySQL
var useInMemory = builder.Configuration.GetValue<bool>("UseInMemory", true);

builder.Services.AddDbContext<CatalogDBContext>(options =>
{
    if (useInMemory)
    {
        options.UseInMemoryDatabase("CatalogDB");
    }
    else
    {
        // Configura para MySQL
        options.UseMySql(builder.Configuration.GetConnectionString("MysqlConnection"), new MySqlServerVersion(new Version(8, 0, 31)));
    }
});

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

// Add cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var app = builder.Build();

// Seed de dados usando banco em memória
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CatalogDBContext>();
    if (useInMemory)
    {
        await DbSeeder.SeedAsync(db);
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthorization();

// Roteamento para MVC (Views)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Roteamento para APIs (atribute routing)
app.MapControllers();

await app.RunAsync();
