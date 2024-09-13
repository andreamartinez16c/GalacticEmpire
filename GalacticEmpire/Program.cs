using GalacticEmpire.Data;
using GalacticEmpire.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionString = builder.Configuration.GetConnectionString("GalacticEmpireDBLocal");
//string connectionString = builder.Configuration.GetConnectionString("GalacticEmpireDBLocal");
builder.Services.AddTransient<IHabitantRepository, HabitantRepository>();
builder.Services.AddTransient<ISpecieRepository, SpecieRepository>();
builder.Services.AddTransient<IPlanetRepository, PlanetRepository>();
builder.Services.AddDbContext<GalacticEmpireContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();

builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Api Galactic Empire",
        Version = "v1"
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint(
        url: "/swagger/v1/swagger.json", name: "Galactic Empire API V1");
    options.RoutePrefix = "";
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
