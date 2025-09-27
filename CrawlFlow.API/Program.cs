using Scalar.AspNetCore;
using CrawlFlow.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


// MongoDB configuration
var mongoSettings = builder.Configuration.GetSection("MongoDb");
var mongoConnectionString = mongoSettings.GetValue<string>("ConnectionString");
var mongoDatabaseName = mongoSettings.GetValue<string>("DatabaseName");
builder.Services.AddSingleton(new MongoContext(mongoConnectionString, mongoDatabaseName));

// PostgreSQL configuration
var pgSettings = builder.Configuration.GetSection("PostgreSql");
var pgConnectionString = pgSettings.GetValue<string>("ConnectionString");
builder.Services.AddDbContext<PostgreSqlContext>(options =>
    options.UseNpgsql(pgConnectionString));

builder.Services.AddControllers();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(); // Map Scalar UI
}

app.UseHttpsRedirection();
app.MapControllers();
app.MapStaticAssets();
app.UseCors(policy => policy
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
app.UseAuthentication();
app.UseAuthorization();

app.Run();