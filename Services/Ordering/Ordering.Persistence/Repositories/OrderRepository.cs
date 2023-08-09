
using Ordering.Persistence.Abstraction;
using Ordering.Persistence.Models;
using Contracts.Ordering;
using Rebus.Bus;
using Contracts.Ordering.Events;

namespace Ordering.Persistence.Repositories;

public interface IOrderRepository
{
    void Create(Order order);
}


public class OrderRepository : IOrderRepository
{
    private readonly IOrderingDbContext context;
    private readonly IBus bus;

    public OrderRepository(IOrderingDbContext context, IBus bus)
    {
        this.context = context;
        this.bus = bus;
    }

    public void Create(Order order)
    {
        context.Orders.Add(order);
        context.SaveChanges();

        bus.Advanced.Topics.Publish(
            OrderingEventNames.OrderCreated,
            new OrderCreatedIntegrationEvent(
                order.Id.Value, 
                order.Items.Select(o => 
                        new OrderItem(o.ProductId, o.Quantity)).ToList()));
    }

}