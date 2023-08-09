using Core.Common;
using Core.Common.Domain;
using Ordering.Persistence.Models.Entities;
using Ordering.Persistence.Models.Enums;
using Ordering.Persistence.Models.ValueObjects;

namespace Ordering.Persistence.Models;


public sealed record OrderDomainError(string Message) : Error(Message);
public sealed class Order : Entity<OrderId>, IRoot
{
    public IReadOnlyCollection<OrderItem> Items => items.ToList().AsReadOnly();
    private readonly ICollection<OrderItem> items;
    public OrderStatus Status { get; private set; }
    public OrderNumber OrderNumber { get; private set; }

    public Order()
    {
        items = new HashSet<OrderItem>();
    }

    public Order(OrderId id, OrderNumber orderNumber, ICollection<OrderItem> items, OrderStatus status, DateTime createdAt) : base(id, createdAt)
    {
        this.items = items;
        Status = status;
        OrderNumber = orderNumber;

    }

    public static Result<Order> Create(ICollection<OrderItem> items, OrderStatus status, DateTime createdAt)
    {

        if (!items.Any())
        {
            return new OrderDomainError("Order itmes must contain least 1 item!");
        }

        var key = new Random().Next(100000, 999999);
        return new Order(new(Guid.NewGuid()), new($"ORDER_{key}"), items, status, createdAt);
    }



    public void Add(OrderItem item)
    {
        var orderItem = items.FirstOrDefault(p => p.ProductId == item.ProductId);
        if (orderItem is not null)
        {
            items.Remove(orderItem);
            orderItem.ChangeQuantity(orderItem.Quantity + item.Quantity);
            items.Add(orderItem);
            return;
        }
        items.Add(item);
    }


    public void Remove(OrderItem item)
    {
        items.Remove(item);
    }
}



