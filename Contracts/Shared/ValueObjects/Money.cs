using Shared.Common.Enums;

namespace Contracts.Shared.ValueObjects;
public sealed class Money
{
    public Currencies CurrencyType { get; set; }
    public decimal Amount { get; set; }
}