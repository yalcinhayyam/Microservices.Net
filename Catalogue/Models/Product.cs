

using Library.UnitOfWork;

namespace Catalogue.Models;



public record ProductInputModel(string Title, ICollection<Money> Prices, Unit Stock);
public record ProductPayloadModel(Guid Id, string Title, IReadOnlyCollection<Money> Prices, Unit Stock);

public sealed class Product : Entity
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public ICollection<Money> Prices { get; private set; }
    public Unit Stock { get; private set; }

    private Product() { }
    
    public Product(Guid id, string title, Unit stock, DateTime dateOfCreate)
    {
        Id = id;
        Title = title;
        Stock = stock;
        DateOfCreate = dateOfCreate;
        Prices = new HashSet<Money>();
    }

    public Product AddPrice(Money money)
    {
        Prices.Add(money);
        return this;
    }
    public static Product Create(string title, Unit stock, DateTime dateOfCreate)
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

