using Catalogue.Application.Features.Catalogue.Commands.CreateProduct;
using Catalogue.Domain;
using Catalogue.Domain.ValueObjects;
using Contracts.Catalogue.Api.CreateProduct;
using Core.Common;
using Core.Common.Services;
using Mapster;
using Shared.Common.Enums;
using Money = Shared.Common.ValueObjects.Money;
using MoneyModel = Shared.Common.ValueObjects.MoneyModel;
namespace Catalogue.Application.Mappings;

public class ProductMappingConfig : IRegister
{


    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Product, ProductPayloadModel>()
        .MapWith((src) => new(src.Id.Value, src.Title, src.Prices.Select(p => new MoneyModel() { CurrencyType = p.CurrencyType, Amount = p.Amount }).ToList().AsReadOnly(), (UnitType)src.Stock.UnitType.Id, src.Stock.Amount));
        config.NewConfig<Product, ProductResult>()
         .MapToConstructor(true)
            .ConstructUsing((src) => new ProductResult(src.Id.Value, src.Title, src.Prices.ToList().AsReadOnly(), src.Stock));

        config.NewConfig<ProductInputModel, CreateProductCommand>()
         .MapToConstructor(true)
            .ConstructUsing((src) => new(src.Title, src.Prices.Select(p => new Money(p.CurrencyType, p.Amount)).ToList().AsReadOnly(), new(src.StockAmount, Enumeration.FromValue<Domain.Enums.UnitType>((int)src.UnitType))));


        config.NewConfig<ProductResult, ProductPayloadModel>()
            .MapWith(
                (src) => new(src.Id, src.Title,
                src.Prices.Select(p => new MoneyModel() { CurrencyType = p.CurrencyType, Amount = p.Amount }).ToList().AsReadOnly(), 
                (UnitType)src.Stock.UnitType.Id, src.Stock.Amount));


        config.NewConfig<CreateProductCommand, Product>()
         .MapToConstructor(true)
            .ConstructUsing((src) => Product.Create(src.Title, src.Stock, MapContext.Current.GetService<IDateTimeProvider>().UtcNow));

        config.NewConfig<ProductUnit, ProductUnit>().MapWith((src) => new(src.Amount, src.UnitType));
        config.NewConfig<Money, Money>().MapWith((src) => new Money(src.CurrencyType, src.Amount));

    }
}