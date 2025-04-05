using Api.Data;
using Api.Data.CompiledModels;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Data
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options
    .UseModel(AppDbContextModel.Instance)
    .EnableServiceProviderCaching()
    .UseNpgsql(builder.Configuration.GetConnectionString("AppConnectionString"));

});
#endregion

#region CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "Default", policy =>
    {
        policy
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});
#endregion

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.MaxDepth = 64;
    });
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
