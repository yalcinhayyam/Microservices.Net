using Catalogue.Models.Enums;

namespace Catalogue.Models.ValueObjects;

public sealed record Money(Currencies CurrencyType, decimal Amount)
{

    public string Currency => Enum.GetName(typeof(Currencies), CurrencyType)!;
    public static implicit operator string(Money money) => Enum.GetName(typeof(Currencies), money.CurrencyType)!;
    public static Money operator *(int quantity, Money money) => new(money.CurrencyType, money.Amount * quantity);
    public static Money operator +(Money current, Money other) => current.CurrencyType == other.CurrencyType ? new(current.CurrencyType, current.Amount + other.Amount) : throw new InvalidOperationException("Cannot add money with different currencies.");
}
