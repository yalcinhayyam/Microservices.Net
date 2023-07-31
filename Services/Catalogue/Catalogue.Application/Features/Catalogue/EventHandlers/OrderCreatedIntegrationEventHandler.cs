using MediatR;
using Core.EventBus.Abstraction;
using Catalogue.Application.Features.Catalogue.Commands.UpdateStock;

public sealed class OrderCreatedIntegrationEventHandler : IIntegrationEventHandler<OrderCreatedIntegrationEvent>
{
    private readonly ISender mediator;

    public OrderCreatedIntegrationEventHandler(ISender mediator)
    {
        this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task Handle(OrderCreatedIntegrationEvent @event)
    {
        await mediator.Send(new UpdateStockCommand(@event.OrderId, @event.OrderItems));
    }
}