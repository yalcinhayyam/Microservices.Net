

using Library.EventBus;
using Library.EventBus.EventHandlers;
using Library.EventBus.Events;
using Rebus.Bus;
using Rebus.Config;

var builder = WebApplication.CreateBuilder(args);

{
    var services = builder.Services;
    services.Configure<EventBusOptions>(builder.Configuration.GetSection("EventBusOptions"));

    var configuration = builder.Configuration;

    services.RegisterRebus(
        onCreated: async (bus) =>
        {
            await bus.Subscribe<ExampleIntegrationEvent>();

        }
    ).AddRebusHandler<ExampleIntegrationEventHandler>();
}

var app = builder.Build();


app.MapGet("/event", (IBus bus) =>
{
    bus.Publish(new ExampleIntegrationEvent { Value = "Hello World" });
    return "Event Sended";
});

app.Run();



