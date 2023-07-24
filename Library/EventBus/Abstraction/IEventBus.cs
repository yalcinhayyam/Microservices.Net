using System;
using System.Text.Json.Serialization;
using Rebus.Handlers;

namespace Library.EventBus.Abstraction;


public record IntegrationEvent
{
    public IntegrationEvent()
    {
        Id = Guid.NewGuid();
        CreationDate = DateTime.UtcNow;
    }

    [JsonConstructor]
    public IntegrationEvent(Guid id, DateTime createDate)
    {
        Id = id;
        CreationDate = createDate;
    }

    [JsonInclude]
    public Guid Id { get; private init; }

    [JsonInclude]
    public DateTime CreationDate { get; private init; }
}



public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
    where TIntegrationEvent : IntegrationEvent
{
    Task Handle(TIntegrationEvent @event);
}

public interface IIntegrationEventHandler : IHandleMessages
{
}

public interface IEventBus
{
    Task Publish(IntegrationEvent @event);

    Task Subscribe<T, TH>()
        where T : IntegrationEvent
        where TH : IIntegrationEventHandler<T>;

    Task Unsubscribe<T, TH>()
        where TH : IIntegrationEventHandler<T>
        where T : IntegrationEvent;
}
