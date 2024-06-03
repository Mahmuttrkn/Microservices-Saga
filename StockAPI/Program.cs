using MassTransit;
using MongoDB.Driver;
using StockAPI;
using StockAPI.Models;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddMassTransit(configure =>
{
    configure.UsingRabbitMq((context, configurator) =>
    {
        configurator.Host(configuration.GetConnectionString("RabbitMQ"));
    });
});

builder.Services.AddHostedService<MassTransitHostedService>();
builder.Services.AddSingleton<MongoDbService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using(var scope = app.Services.CreateScope())
{
    var mongoDbService = scope.ServiceProvider.GetRequiredService<MongoDbService>();

    // Use an empty filter to check if any documents exist in the collection
    if (!mongoDbService.GetCollection<Stock>().Find(FilterDefinition<Stock>.Empty).Any())
    {
        mongoDbService.GetCollection<Stock>().InsertOne(new Stock
        {
            ProductId = 21,
            Count = 200
        });
        mongoDbService.GetCollection<Stock>().InsertOne(new Stock
        {
            ProductId = 22,
            Count = 100
        });
        mongoDbService.GetCollection<Stock>().InsertOne(new Stock
        {
            ProductId = 23,
            Count = 50
        });
        mongoDbService.GetCollection<Stock>().InsertOne(new Stock
        {
            ProductId = 24,
            Count = 10
        });
        mongoDbService.GetCollection<Stock>().InsertOne(new Stock
        {
            ProductId = 25,
            Count = 30
        });
    }
}

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
