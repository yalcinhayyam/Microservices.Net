using Contracts.Ordering.ValueObjects;
using Core.Common.Domain;
using Ordering.Persistence.Models.Enums;
using Ordering.Persistence.Models.ValueObjects;


namespace Ordering.Persistence.Models;

public sealed class Order : Entity<OrderId>, IRoot
{
    private readonly ICollection<OrderItem> items;
    public IReadOnlyCollection<OrderItem> Items => items.ToList().AsReadOnly();
    public OrderStatus status { get; set; }

    public Order(OrderId id, ICollection<OrderItem> items, DateTime createdAt) : base(id, createdAt)
    {
        items = new HashSet<OrderItem>();
    }

    public Order Create(ICollection<OrderItem> items, DateTime createdAt)
        => new(new(Guid.NewGuid()), items, createdAt);



    public void Add(OrderItem item)
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


    public void Remove(OrderItem item)
    {
        items.Remove(item);
    }
}



