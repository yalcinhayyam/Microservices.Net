using Library.EventBus.Abstraction;
using Rebus.Activation;
using Rebus.Bus;
using Rebus.Config;
using Rebus.RabbitMq;
using Rebus.Retry.Simple;
using Microsoft.Extensions.Options;
using Rebus.Routing.TypeBased;
using System.Reflection;

namespace Library.EventBus;

public record EventBusOptions
{
    public required string ConnectionString { get; set; }
    public required string QueueName { get; set; }

    public void Deconstruct(out string ConnectionString, out string QueueName)
    {
        ConnectionString = this.ConnectionString;
         QueueName = this.QueueName;
    }
}

public class RebusEventBusAdapter : IEventBus
{
    private readonly IBus bus;
    private readonly EventBusOptions eventBusOptions;

    public RebusEventBusAdapter(IOptions<EventBusOptions> options, IBus bus)
    {
        eventBusOptions = options.Value;
        this.bus = bus;
    }

    public async Task Publish(IntegrationEvent @event)
    {
        bus.Publish(@event).Wait();
    }

    public async Task Subscribe<T, TH>()
        where T : IntegrationEvent
        where TH : IIntegrationEventHandler<T>
    {
        await bus.Subscribe<T>();
    }

    public async Task Unsubscribe<T, TH>()
        where TH : IIntegrationEventHandler<T>
        where T : IntegrationEvent
    {
        await bus.Unsubscribe<T>();
    }

    public void Dispose()
    {
        bus?.Dispose();
    }
}
