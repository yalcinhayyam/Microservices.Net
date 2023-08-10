using Shared.Common.Enums;
using Shared.Common.ValueObjects;

namespace Contracts.Catalogue.Api.CreateProduct;

public sealed record CreateProductInput(
        string Title,
        ICollection<MoneyModel> Prices,
        UnitType UnitType,
        decimal StockAmount
    );
