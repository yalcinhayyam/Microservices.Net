using Contracts.Ordering.ValueObjects;
using Core.EventBus.Abstraction;

public sealed record OrderCreatedIntegrationEvent(
        Guid OrderId,
        IReadOnlyCollection<OrderItem> OrderItems
    ) : IntegrationEvent ;

