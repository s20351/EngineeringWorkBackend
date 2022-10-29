using Application.Services.Cycles;
using Application.Services.DateDelivery;
using Application.Services.Exports;
using Application.Services.Farmers;
using Application.Services.Farms;
using Application.Services.OrdersFeed;
using Application.Services.OrdersHatchery;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:5173" , "http://localhost:5174");
                      });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// dependency injection
builder.Services.AddTransient<IFarmersDatabaseService, FarmersDatabaseService>();
builder.Services.AddTransient<IFarmsDatebaseService, FarmsDatabaseService>();
builder.Services.AddTransient<ICycleDatabaseService, CycleDatabaseService>();
builder.Services.AddTransient<IExportDatabaseService, ExportDatabaseService>();
builder.Services.AddTransient<IOrderHatcheryDatabaseService, OrderHatcheryDatabaseService>();
builder.Services.AddTransient<IOrderFeedDatabaseService, OrderFeedDatabaseService>();
builder.Services.AddTransient<IDateDeliveryDatabaseService, DateDeliveryDatabaseService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
