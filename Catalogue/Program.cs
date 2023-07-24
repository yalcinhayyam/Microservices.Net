using Library.EventBus;
using Library.EventBus.Events;
using Rebus.Bus;


var builder = WebApplication.CreateBuilder(args);

{
    var services = builder.Services;
    services.Configure<EventBusOptions>(builder.Configuration.GetSection("EventBusOptions"));

    var configuration = builder.Configuration;

    services.RegisterRebus();
}

var app = builder.Build();


app.MapGet("/event", (IBus bus) =>
{
    bus.Publish(new ExampleIntegrationEvent { Value = "Hello World" });
    return "Event Sended";
});

app.Run();
