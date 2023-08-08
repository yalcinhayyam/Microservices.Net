
using Shared.Common.ValueObjects;

namespace Graphql.Models;
public class Product
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public decimal StockAmount { get; set; }
    public UnitType StockUnit { get; set; }
    public ICollection<Money> Prices { get; set; }
    public DateTime CreateAt { get; set; }
    public virtual ICollection<OrderItem> OdrerItems { get; set; }
}

public class OrderItem
{
    Guid ProductId { get; set; }
    decimal Amount { get; set; }
    public virtual Product Product { get; set; }
}
