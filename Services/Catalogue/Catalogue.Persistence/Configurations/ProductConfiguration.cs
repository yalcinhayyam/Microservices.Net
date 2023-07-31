using Catalogue.Domain;
using Catalogue.Domain.ValueObjects;
using Catalogue.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;


namespace Catalogue.Persistence.Configurations;


public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {

        builder.ToTable(nameof(CatalogueDbContext.Products));
        var productIdConverter = new ValueConverter<ProductId, Guid>(
                    id => id.Value,
                    id => new(id)
                );
        builder.Property(o => o.Id)
          .HasConversion(productIdConverter);

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Title)
               .HasMaxLength(100).HasColumnName(nameof(Product.Title));


        builder.OwnsMany(p => p.Prices, pricesBuilder =>
        {
            pricesBuilder.WithOwner().HasForeignKey("ProductId");
            pricesBuilder.ToTable("Prices");
        });

        builder.OwnsOne(p => p.Stock, stockBuilder =>
        {
            stockBuilder.WithOwner();
            stockBuilder.Property(p => p.Amount).HasColumnName("Stock_Amount");
            stockBuilder.Property(p => p.UnitType).HasColumnName("Stock_UnitType");
        }).Navigation(p => p.Stock).IsRequired();
    }
}