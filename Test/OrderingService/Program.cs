using Rebus.Activation;
using Rebus.Bus;
using Rebus.Config;
using Rebus.Transport;
using Contract.Events.Ordering;

using var activator = new BuiltinHandlerActivator();

var rebus = ConfigureRebus(activator);


Console.WriteLine("Press 'Q' to exit.");

while (true)
{
    var key = Console.ReadKey();
    if (key.Key == ConsoleKey.Q)
    {
        break;
    }

    using (var scope = new RebusTransactionScope())
    {
        await rebus.Advanced.Topics.Publish("event-bus", new OrderPlacedEvent { OrderId = "12345" });
        scope.Complete();
    }
}



IBus ConfigureRebus(BuiltinHandlerActivator activator)
{

    var rebus = Configure.With(activator)
        .Logging(l => l.ColoredConsole())
        .Transport(t => t.UseRabbitMq("amqp://guest:guest@localhost:5672", "ordering-service"))
        .Start();

    return rebus;
}
