using Application.Services.Cycles;
using Application.Services.DateDelivery;
using Application.Services.Exports;
using Application.Services.Farmers;
using Application.Services.Farms;
using Application.Services.OrdersFeed;
using Application.Services.OrdersHatchery;
using Azure.Identity;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin();
                          policy.AllowAnyHeader();
                          policy.AllowAnyMethod();
                          policy.WithMethods("GET", "PUT", "DELETE", "POST", "PATCH");
                      });
});


builder.Services.AddDbContext<pjatkContext>
        (options => options.UseSqlServer(builder.Configuration.GetConnectionString("pjatkDb")));

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles); 

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureSwaggerGen(setup =>
{
    setup.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Wstawienia indos",
        Version = "v1"
    });
});

// dependency injection
builder.Services.AddTransient<IFarmersDatabaseService, FarmersDatabaseService>();
builder.Services.AddTransient<IFarmsDatebaseService, FarmsDatabaseService>();
builder.Services.AddTransient<ICycleDatabaseService, CycleDatabaseService>();
builder.Services.AddTransient<IExportDatabaseService, ExportDatabaseService>();
builder.Services.AddTransient<IOrderHatcheryDatabaseService, OrderHatcheryDatabaseService>();
builder.Services.AddTransient<IOrderFeedDatabaseService, OrderFeedDatabaseService>();
builder.Services.AddTransient<IDeliveryDatabaseService, DeliveryDatabaseService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lotus.API.Integration v1"));


app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
