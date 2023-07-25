using MapsterMapper;
using Mapster;
using Catalogue.Models;
using Library.UnitOfWork;

public class ProductMappingConfig : IRegister
{


    public void Register(TypeAdapterConfig config)
    {

        config.NewConfig<Product, ProductPayloadModel>()
         .MapToConstructor(true)
            .ConstructUsing((src) => new ProductPayloadModel(src.Id, src.Title, src.Prices.ToList().AsReadOnly(), src.Stock));

        config.NewConfig<ProductInputModel, Product>()
         .MapToConstructor(true)
            .ConstructUsing((src) => Product.Create(src.Title, src.Stock, MapContext.Current.GetService<IDateTimeProvider>().UtcNow));

            config.NewConfig<Unit,Unit>().MapToConstructor(true).ConstructUsing((src) => new(src.Amount,src.Type));
            config.NewConfig<Money,Money>().MapToConstructor(true).ConstructUsing((src) => new(src.CurrencyType,src.Amount));

    }
}