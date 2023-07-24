using Library.EventBus.Events;
using Rebus.Handlers;

namespace Library.EventBus.EventHandlers;
public class ExampleIntegrationEventHandler : IHandleMessages<ExampleIntegrationEvent>
{
    public async Task Handle(ExampleIntegrationEvent message)
    {
        System.Console.WriteLine(message);
    }
}

