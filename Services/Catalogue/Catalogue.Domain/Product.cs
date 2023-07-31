
using Catalogue.Domain.ValueObjects;
using Core.Common.Domain;
using Shared.Common.ValueObjects;

namespace Catalogue.Domain;
public class Product : Entity<ProductId>, IRoot
{
    public string Title { get; private set; }
    public ICollection<Money> Prices { get; private set; }
    public ProductUnit Stock { get; private set; }
    private Product() { }

    public Product(ProductId id, string title, ProductUnit stock, DateTime createdAt) : base(id, createdAt)
    {
        Title = title;
        Stock = stock;
        Prices = new HashSet<Money>();
    }

    public Product AddPrice(Money money)
    {
        Prices.Add(money);
        return this;
    }
    public Product RemoveStockByAmount(decimal amount)
    {
        Stock -= new ProductUnit(amount, Stock.UnitType);
        return this;
    }

    public Product RemoveStock(ProductUnit stock)
    {
        Stock -= stock;
        return this;
    }

    public static Product Create(string title, ProductUnit stock, DateTime createdAti)
        => new(new(Guid.NewGuid()), title, stock, createdAti);

    public Product ChangePrice(Money price)
    {
        var exists = Prices.FirstOrDefault(p => p.CurrencyType == price.CurrencyType);
        if (exists is not null)
        {
            Prices.Remove(exists);
        }

        Prices.Add(price);
        return this;

    }

}



file sealed class ProductEquality
{

    public Guid Id { get; set; }
    public string Title { get; set; }
    public int Stock { get; set; }
    public static bool operator ==(ProductEquality first, ProductEquality other) => first.Id == other.Id;

    public static bool operator !=(ProductEquality first, ProductEquality other) => !(first == other);
    public override bool Equals(object obj)
    {
        if (obj is ProductEquality other)
        {
            return this == other;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return Title.GetHashCode();
    }

}

