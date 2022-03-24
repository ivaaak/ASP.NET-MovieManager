using Microsoft.EntityFrameworkCore;
using MovieManager.Api.Controllers;
using MovieManager.Data;
using MovieManager.Data.DBConfig;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<MovieApiController>();
builder.Services.AddDbContext<MovieContext>(options => options.UseSqlServer(Configuration.ConnectionString));
builder.Services.AddControllers();

// Swagger/OpenAPI - https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();