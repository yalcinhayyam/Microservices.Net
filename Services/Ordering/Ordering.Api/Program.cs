
using Core.EventBus;
using Ordering.Contexts;
using Ordering.Persistence.Abstraction;
using Ordering.Persistence.Repositories;
using Rebus.Config;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

{
    var services = builder.Services;
    services.Configure<EventBusOptions>(builder.Configuration.GetSection("EventBusOptions"));
    services.AddSingleton<IOrderingDbContext, OrderingDbContext>();
    services.AddSingleton<IOrderRepository, OrderRepository>();

    var configuration = builder.Configuration;

    services.AddRebus(
        (configurer, provider) =>
        {
            var (ConnectionString, QueueConnectionName) = provider.GetRequiredService<IOptions<EventBusOptions>>().Value;
            return configurer
                    .Logging(l => l.ColoredConsole())
                    .Transport(t => t.UseRabbitMq(ConnectionString, QueueConnectionName));
        });
}

var app = builder.Build();


app.MapGet("/create-order", (IOrderRepository repository) =>
{
    
    // repository.Create()
    return "Event Sended";
});



app.Run();



