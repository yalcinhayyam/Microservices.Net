using Rebus.Activation;
using Rebus.Bus;
using Rebus.Config;
using Rebus.Handlers;
using Rebus.Routing.TypeBased;
using Contract.Events.Ordering;



using var activator = new BuiltinHandlerActivator();

activator.Register(() => new InventoryNotificationHandler());

var rebus = ConfigureRebus(activator);
await activator.Bus.Advanced.Topics.Subscribe("event-bus");


Console.ReadKey();



IBus ConfigureRebus(BuiltinHandlerActivator activator)
{

    var rebus = Configure.With(activator)
        .Logging(l => l.ColoredConsole())
        // .Transport(t => t.UseInMemoryTransport(new InMemNetwork(), "event-bus"))
        .Transport(t => t.UseRabbitMq("amqp://guest:guest@localhost:5672", "inventory-service"))
        .Routing(r => r.TypeBased().Map<OrderPlacedEvent>("event-bus"))
        .Start();

    return rebus;
}




public class InventoryNotificationHandler : IHandleMessages<OrderPlacedEvent>
{
    public Task Handle(OrderPlacedEvent message)
    {
        Console.WriteLine($"Inventory updated for Order ID: {message.OrderId}");
        return Task.CompletedTask;
    }
}





