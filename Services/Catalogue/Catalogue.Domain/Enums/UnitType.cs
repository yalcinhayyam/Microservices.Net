using Core.Common;

namespace Catalogue.Domain.Enums;
public class UnitType : Enumeration
{
    public static UnitType Kg = new(1, nameof(Kg));
    public static UnitType Litre = new(2, nameof(Litre));
    public static UnitType Piece = new(3, nameof(Piece));
    public static UnitType Box = new(4, nameof(Box));
    public UnitType(int id, string name) : base(id, name) { }
}

