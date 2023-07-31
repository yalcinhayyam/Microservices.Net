using Catalogue.Application.Features.Catalogue.Commands.CreateProduct;
using Catalogue.Domain;
using Catalogue.Domain.ValueObjects;
using Contracts.Catalogue.Api.CreateProduct;
using Core.Common.Services;
using Mapster;
using Shared.Common.ValueObjects;

namespace Catalogue.Api.Mappings;
public class ProductMappingConfig : IRegister
{


    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Product, ProductPayloadModel>()
        .MapWith((src) => new(src.Id.Value, src.Title, src.Prices.ToList().AsReadOnly(), src.Stock.UnitType,src.Stock.Amount));
        config.NewConfig<Product, ProductResult>()
         .MapToConstructor(true)
            .ConstructUsing((src) => new ProductResult(src.Id.Value, src.Title, src.Prices.ToList().AsReadOnly(), src.Stock));

        config.NewConfig<ProductInputModel, CreateProductCommand>()
         .MapToConstructor(true)
            .ConstructUsing((src) => new(src.Title, src.Prices, new(src.StockAmount, src.UnitType)));


        config.NewConfig<ProductResult, ProductPayloadModel>()
            .MapWith((src) => new(src.Id, src.Title, src.Prices, src.Stock.UnitType,src.Stock.Amount));


        config.NewConfig<CreateProductCommand, Product>()
         .MapToConstructor(true)
            .ConstructUsing((src) => Product.Create(src.Title, src.Stock, MapContext.Current.GetService<IDateTimeProvider>().UtcNow));

        config.NewConfig<ProductUnit, ProductUnit>().MapWith((src) => new(src.Amount, src.UnitType));
        config.NewConfig<Money, Money>().MapWith((src) => new(src.CurrencyType, src.Amount));

    }
}