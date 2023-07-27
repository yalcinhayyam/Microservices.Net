using Catalogue.Models.Enums;

namespace Catalogue.Models.ValueObjects;



public sealed record ProductUnit(decimal Amount, UnitTypes UnitType)
{
    public string Type => Enum.GetName(typeof(UnitTypes), UnitType)!;
    public static implicit operator string(ProductUnit unit) => Enum.GetName(typeof(UnitTypes), unit.UnitType)!;
}


