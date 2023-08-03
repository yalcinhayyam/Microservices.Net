using Core.EventBus.Abstraction;

public sealed record OrderItem(Guid ProductId, decimal Amount);

public sealed record OrderCreatedIntegrationEvent(
        Guid OrderId,
        IReadOnlyCollection<OrderItem> OrderItems
    ) : IntegrationEvent ;

