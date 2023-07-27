

using Catalogue.Models.ValueObjects;
using Library.UnitOfWork;

namespace Catalogue.Models;


public sealed record ProductInputModel(string Title, ICollection<Money> Prices, ProductUnit Stock);
public sealed record ProductPayloadModel(Guid Id, string Title, IReadOnlyCollection<Money> Prices, ProductUnit Stock);

public class Product : Entity
{
    public string Title { get; protected set; }
    public ICollection<Money> Prices { get; protected set; }
    public ProductUnit Stock { get; protected set; }


    protected Product() { }

    public Product(Guid id, string title, ProductUnit stock, DateTime createdAt) : base(id, createdAt)
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
    public static Product Create(string title, ProductUnit stock, DateTime dateOfCreate)
        => new(Guid.NewGuid(), title, stock, dateOfCreate);

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

