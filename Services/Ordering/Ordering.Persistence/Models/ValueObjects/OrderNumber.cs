namespace Ordering.Persistence.Models.ValueObjects;


public sealed record OrderNumber
{

    public string Value { get; init; }
    public OrderNumber(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentNullException(nameof(value));
        }
        if (value.Length != 12)
        {
            throw new ArgumentException("Order number must 6 digit alphanumeric", nameof(value));
        }
        Value = value;
    }
}