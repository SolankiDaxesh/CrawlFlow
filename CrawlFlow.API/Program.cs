using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


// MongoDB configuration
var mongoSettings = builder.Configuration.GetSection("MongoDb");
var connectionString = mongoSettings.GetValue<string>("ConnectionString");
var databaseName = mongoSettings.GetValue<string>("DatabaseName");

builder.Services.AddSingleton(new CrawlFlow.Infrastructure.MongoContext(connectionString, databaseName));
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