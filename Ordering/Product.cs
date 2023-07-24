
record Product(Guid Id, string Title, Money Price, int Amount) { }

sealed record Money(Currencies Currency, decimal Amount)
{
    public static Money operator *(int quantity, Money money) => new Money(money.Currency, money.Amount * quantity);
    public static Money operator +(Money current, Money other) => current.Currency == other.Currency ? new Money(current.Currency, current.Amount + other.Amount) : throw new InvalidOperationException("Cannot add money with different currencies.");
}

enum Currencies
{
    TL, Euro, Dolar
}







file class ProductEquality
{

    public Guid Id { get; set; }
    public string Title { get; set; }
    public int Amount { get; set; }
    public static bool operator  ==(ProductEquality first, ProductEquality other) => first.Id == other.Id;

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


