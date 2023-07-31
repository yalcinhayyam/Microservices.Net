

using Catalogue.Domain.Enums;


namespace Catalogue.Domain.ValueObjects;

public sealed record ProductUnit(decimal Amount, UnitType UnitType)
{


#if false
    public string Type => Enum.GetName(typeof(UnitType), UnitType)!;
    public static implicit operator string(ProductUnit unit) => Enum.GetName(typeof(UnitType), unit.UnitType)!;
#endif

    public static ProductUnit operator -(ProductUnit left, ProductUnit right)
    {
        if (left.UnitType != right.UnitType)
        {
            throw new ArgumentException("Cannot subtract units with different types.");
        }

        decimal resultAmount = left.Amount - right.Amount;
        return new ProductUnit(resultAmount, left.UnitType);
    }

    public static ProductUnit operator +(ProductUnit left, ProductUnit right)
    {
        if (left.UnitType != right.UnitType)
        {
            throw new ArgumentException("Cannot subtract units with different types.");
        }

        decimal resultAmount = left.Amount + right.Amount;
        return new ProductUnit(resultAmount, left.UnitType);
    }

}


