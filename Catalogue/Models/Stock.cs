namespace Catalogue.Models;


public sealed record Unit(decimal Amount, UnitTypes Type)
{
    public string UnitType => Enum.GetName(typeof(UnitTypes), Type)!;
    public static implicit operator string(Unit unit) => Enum.GetName(typeof(UnitTypes), unit.UnitType)!;
}


