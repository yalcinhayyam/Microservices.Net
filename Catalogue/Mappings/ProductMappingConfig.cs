using MapsterMapper;
using Mapster;
using Catalogue.Models;
using Library.UnitOfWork;
using Catalogue.Models.ValueObjects;

public class ProductMappingConfig : IRegister
{


    public void Register(TypeAdapterConfig config)
    {

        config.NewConfig<Product, ProductResult>()
         .MapToConstructor(true)
            .ConstructUsing((src) => new ProductResult(src.Id, src.Title, src.Prices.ToList().AsReadOnly(), src.Stock));

        config.NewConfig<ProductInputModel, CreateProductCommand>()
         .MapToConstructor(true);

        config.NewConfig<ProductResult, ProductPayloadModel>()
         .MapToConstructor(true);

        config.NewConfig<CreateProductCommand, Product>()
         .MapToConstructor(true)
            .ConstructUsing((src) => Product.Create(src.Title, src.Stock, MapContext.Current.GetService<IDateTimeProvider>().UtcNow));

        config.NewConfig<ProductUnit, ProductUnit>().MapToConstructor(true).ConstructUsing((src) => new(src.Amount, src.UnitType));
        config.NewConfig<Money, Money>().MapToConstructor(true).ConstructUsing((src) => new(src.CurrencyType, src.Amount));

    }
}