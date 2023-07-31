namespace Graphql.Models;


public class ProductQuery
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public decimal Amount { get; set; }
    public UnitTypes UnitType { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<Money> Prices { get; set; }
}