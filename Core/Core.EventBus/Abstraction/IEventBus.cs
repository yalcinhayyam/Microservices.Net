namespace Core.EventBus.Abstraction;


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
