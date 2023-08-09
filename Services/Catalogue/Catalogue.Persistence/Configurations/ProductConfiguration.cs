using Catalogue.Domain;
using Catalogue.Domain.Enums;

using Catalogue.Persistence.Contexts;
using Core.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Catalogue.Persistence.Configurations;


public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {

        builder.ToTable(nameof(CatalogueDbContext.Products));

        builder.Property(o => o.Id)
          .HasConversion(id => id.Value,
                    id => new(id));

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
            stockBuilder.Property(p => p.Amount).HasColumnName("Stock_Amount");
            stockBuilder.Property(p => p.UnitType)
                .HasColumnName("Stock_UnitType")
                .HasConversion(
                    t => t.Name,
                    name => Enumeration.FromDisplayName<UnitType>(name)
                );

    }).Navigation(p => p.Stock).IsRequired();
}
}