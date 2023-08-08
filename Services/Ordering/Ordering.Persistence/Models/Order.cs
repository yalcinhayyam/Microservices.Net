using Core.Common;
using Core.Common.Domain;
using Ordering.Persistence.Models.Enums;
using Ordering.Persistence.Models.ValueObjects;


namespace Ordering.Persistence.Models;
public sealed record OrderNumber
{

    public string Value { get; init; }
    public OrderNumber(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentNullException(nameof(value));
        }
        if (value.Length != 12)
        {
            throw new ArgumentException("Order number must 6 digit alphanumeric", nameof(value));
        }
        Value = value;
    }
}

public sealed record OrderDomainError(string Message) : Error(Message);
public sealed class Order : Entity<OrderId>, IRoot
{
    public IReadOnlyCollection<ValueObjects.OrderItem> Items => items.ToList().AsReadOnly();
    private readonly ICollection<ValueObjects.OrderItem> items;
    public OrderStatus Status { get; private set; }
    public OrderNumber OrderNumber { get; private set; }

    public Order()
    {
        items = new HashSet<ValueObjects.OrderItem>();
    }

    public Order(OrderId id, OrderNumber orderNumber, ICollection<ValueObjects.OrderItem> items, OrderStatus status, DateTime createdAt) : base(id, createdAt)
    {
        this.items = items;
        this.Status = status;
        this.OrderNumber = orderNumber;

    }

    public static Result<Order> Create(ICollection<ValueObjects.OrderItem> items, OrderStatus status, DateTime createdAt)
    {

        if (!items.Any())
        {
            return new OrderDomainError("Order itmes must contain least 1 item!");
        }

        var key = new Random().Next(100000, 999999);
        return new Order(new(Guid.NewGuid()), new($"ORDER_{key}"), items, status, createdAt);
    }



    public void Add(ValueObjects.OrderItem item)
    {
        var product = items.FirstOrDefault(p => p.ProductId == item.ProductId);
        if (product is not null)
        {
            items.Remove(product);
            items.Add(product with { Amount = product.Amount + item.Amount });
            return;
        }
        items.Add(item);
    }


    public void Remove(ValueObjects.OrderItem item)
    {
        items.Remove(item);
    }
}



