using System.Text.Json.Serialization;
using Library.EventBus.Abstraction;
using Rebus.Activation;
using Rebus.Config;
using Rebus.Handlers;
// using Library.EventBus.EventHandlers;
using Library.EventBus.Events;
using Rebus.Bus;

using var activator = new BuiltinHandlerActivator();
activator.Register(() => new ExampleIntegrationEventHandler());


var subscriber = Configure.With(activator)
    .Transport(t => t.UseRabbitMq("amqp://guest:guest@rabbitmq:5672", "Microservices-Test"))
    .Start();
await subscriber.Subscribe<ExampleIntegrationEvent>();
Console.ReadLine();




public class ExampleIntegrationEventHandler : IHandleMessages<ExampleIntegrationEvent>
{
    private readonly IBus bus;
    public async Task Handle(ExampleIntegrationEvent message)
    {
        //  bus.Advanced.Topics.Publish("",message);
        System.Console.WriteLine(message);
    }
}

