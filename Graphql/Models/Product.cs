using Shared.Common.Enums;
using Shared.Common.ValueObjects;

namespace Graphql.Models;
public class Product //:IOutputType
{
    public DateTime CreatedAt { get; set; }
    public Guid Id { get; set; }
    public string Title { get; set; }
    public decimal StockAmount { get; set; }
    public UnitType StockUnit { get; set; }
    public ICollection<MoneyModel> Prices { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; }
}

public class OrderItem
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public decimal Quantity { get; set; }
    public DateTime CreatedAt { get; set; }

    public virtual Product Product { get; set; }
    public virtual Order Order { get; set; }

}


