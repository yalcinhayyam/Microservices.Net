namespace Graphql.Models;

public class Order
{
    public Guid Id { get; set; }
    public OrderStatus Status { get; set; }
    public string OrderNumber { get; set; }
    public DateTime CreatedAt { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; }
}


public enum OrderStatus
{
    Submitted = 1,
    AwaitingValidation = 2,
    StockConfirmed = 3,
    Paid = 4,
    Shipped = 5,
    Cancelled = 6
}