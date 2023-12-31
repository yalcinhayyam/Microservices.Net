using Shared.Common.Enums;
using Shared.Common.ValueObjects;

namespace Contracts.Catalogue.Api.CreateProduct;

public sealed record CreateProductPayload(
                            Guid Id,
                            string Title,
                            IReadOnlyCollection<MoneyModel> Prices,
                            UnitType UnitType,
                            decimal StockAmount);
