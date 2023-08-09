using Contracts.Catalogue.Enums;
using Contracts.Shared.ValueObjects;

namespace Contracts.Catalogue.Api.CreateProduct;

public sealed record ProductInputModel(
        string Title,
        ICollection<Money> Prices,
        UnitType UnitType,
        decimal StockAmount
    );
