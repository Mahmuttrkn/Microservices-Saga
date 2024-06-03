using MassTransit;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
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
