using Shared.Common.ValueObjects;

namespace Contracts.Catalogue.Api.CreateProduct;

public sealed record ProductInputModel(
        string Title,
        ICollection<Money> Prices,
        UnitType UnitType,
        decimal StockAmount
    );
