using SagaMachine.Service;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using SagaMachine;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        // Adding services to the container
        builder.Services.AddHostedService<Worker>();

        builder.Services.AddMassTransit(configure =>
        {
            configure.AddSagaStateMachine<OrderStateMachine, OrderStateInstance>()
            .EntityFrameworkRepository(options =>
            {
                options.AddDbContext<DbContext, OrderStateDbContext>((provider, dbContextBuilder) =>
                {
                    var configuration = provider.GetRequiredService<IConfiguration>();
                    dbContextBuilder.UseSqlServer(configuration.GetConnectionString("SQLServer"));
                });
            });

            configure.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                cfg.Host(configuration.GetConnectionString("RabbitMQ"));
            }));

            // This line is moved inside AddMassTransit to ensure the service is registered
            builder.Services.AddHostedService<MassTransitHostedService>();
        });

        // Build and run the host
        var host = builder.Build();
        host.Run();
    }
}
