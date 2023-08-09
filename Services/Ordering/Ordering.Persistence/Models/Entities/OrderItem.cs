using Core.Common;
using Core.Common.Domain;
using Ordering.Persistence.Models.ValueObjects;

namespace Ordering.Persistence.Models.Entities;



public sealed record OrderItemId(Guid Value) : AbstractEntityId(Value);
public sealed record OrderItemDomainError(string Message) : Error(Message);

public class OrderItem : Entity<OrderItemId>
{
    public OrderId OrderId { get; private set; }
    public Guid ProductId { get; private set; }

    public decimal Quantity { get; private set; }
    // public Money Price { get; private set; }

    public virtual Order Order { get; private set; }

    private OrderItem() { }

    public OrderItem(OrderItemId id, Guid productId, decimal quantity /*, Money price*/)
        : base(id)
    {
        ProductId = productId;
        Quantity = quantity;

        // ProductId = productId ?? throw new ArgumentNullException(nameof(productId));
        // Price = price ?? throw new ArgumentNullException(nameof(price));
    }

    public static Result<OrderItem> Create(Guid productId, decimal quantity)
    {
        if (quantity <= 0)
        {
            return new OrderItemDomainError("Quantity must be greater than zero.");
        }

        // if (price.Amount <= 0)
        // {
        //     return new OrderItemDomainError("Price must be greater than zero.");
        // }
        return new OrderItem(new(Guid.NewGuid()), productId, quantity);
    }

    public Result ChangeQuantity(decimal newQuantity)
    {
        if (newQuantity <= 0)
        {
            return new OrderItemDomainError("Quantity must be greater than zero.");
        }
        Quantity = newQuantity;
        return Result.Completed;
    }

    // public void ChangePrice(Money newPrice)
    // {
    //     if (newPrice.Amount <= 0)
    //     {
    //         throw new OrderItemDomainError("Price must be greater than zero.");
    //     }

    //     Price = newPrice ?? throw new ArgumentNullException(nameof(newPrice));
    // }
}
