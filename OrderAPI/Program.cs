using MassTransit;
using Microsoft.EntityFrameworkCore;
using OrderAPI;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("SQLServer")));  


// Add services to the container.
builder.Services.AddMassTransit(configure =>
{
    configure.UsingRabbitMq((context, configurator) =>
    {
        configurator.Host(configuration.GetConnectionString("RabbitMQ"));
    });
});
builder.Services.AddHostedService<MassTransitHostedService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
