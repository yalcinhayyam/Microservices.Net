
using Shared.Common.ValueObjects;

namespace Contracts.Catalogue.Api.CreateProduct;

public sealed record ProductPayloadModel(
                            Guid Id,
                            string Title,
                            IReadOnlyCollection<Money> Prices,
                            UnitType UnitType,
                            decimal StockAmount);
