using Rebus.Handlers;

namespace Core.EventBus.Abstraction;


public interface IIntegrationEventHandler : IHandleMessages { }

public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler, IHandleMessages<TIntegrationEvent>
    where TIntegrationEvent : IntegrationEvent
{
    Task Handle(TIntegrationEvent @event);
}
