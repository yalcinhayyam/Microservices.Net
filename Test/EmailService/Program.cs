using Rebus.Activation;
using Rebus.Bus;
using Rebus.Config;
using Rebus.Handlers;
using Rebus.Routing.TypeBased;
using Contract.Events.Ordering;


using var activator = new BuiltinHandlerActivator();

activator.Register(() => new EmailNotificationHandler());

var rebus = ConfigureRebus(activator);
await activator.Bus.Advanced.Topics.Subscribe("event-bus");


Console.ReadKey();



IBus ConfigureRebus(BuiltinHandlerActivator activator)
{

    var rebus = Configure.With(activator)
        .Logging(l => l.ColoredConsole())
        // .Transport(t => t.UseInMemoryTransport(new InMemNetwork(), "email-service"))
        .Transport(t => t.UseRabbitMq("amqp://guest:guest@localhost:5672", "email-service"))
        .Routing(r => r.TypeBased().Map<OrderPlacedEvent>("event-bus"))
        .Start();

    return rebus;
}




public class EmailNotificationHandler : IHandleMessages<OrderPlacedEvent>
{
    public Task Handle(OrderPlacedEvent message)
    {
        Console.WriteLine($"Sending confirmation email for Order ID: {message.OrderId}");
        return Task.CompletedTask;
    }
}





