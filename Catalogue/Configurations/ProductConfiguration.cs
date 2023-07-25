using Catalogue.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Catalogue.Configurations;



public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Title)
               .IsRequired()
               .HasMaxLength(100);
        // builder.Ignore(p => p.Prices);
        builder.OwnsMany(p => p.Prices, pricesBuilder =>
        {
            pricesBuilder.Property(p => p.Amount)
                       .IsRequired();
            pricesBuilder.Property<int>(p => (int)p.CurrencyType)
                         .IsRequired();
        });

        builder.OwnsOne(p => p.Stock, stockBuilder =>
        {
            // stockBuilder.WithOwner().HasForeignKey("StockId");
            stockBuilder.Property(s => s.Type)
                         .IsRequired().HasConversion<string>();
            stockBuilder.Property(s => s.Amount)
                         .IsRequired();
        });



    //     modelBuilder.Entity<Post>().OwnsOne(p => p.AuthorName).HasData(
    // // new { PostId = 1, First = "Andriy", Last = "Svyryd" },
    // // new { PostId = 2, First = "Diego", Last = "Vega" });
    //     modelBuilder.Entity<Product>(
    //         p => p.
    //     ).HasData(

    //        new List<Product>(){
    //             Product.Create("Armut",new(200,UnitType.Kg),dateTimeProvider.UtcNow).AddPrice(new(Currencies.TL,22.5m)),
    //             Product.Create("Ananas",new(50,UnitType.Piece),dateTimeProvider.UtcNow).AddPrice(new(Currencies.TL,30m)),
    //             Product.Create("Hacı Şakir",new(200,UnitType.Box),dateTimeProvider.UtcNow).AddPrice(new(Currencies.TL,52.6m)),
    //             Product.Create("Gül Suyu",new(80,UnitType.Litre),dateTimeProvider.UtcNow).AddPrice(new(Currencies.TL,160.8m)),
    //        }
    //  );


    }
}