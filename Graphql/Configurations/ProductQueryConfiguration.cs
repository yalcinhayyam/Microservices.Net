using Graphql.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Common.Enums;

namespace Catalogue.Configurations;



internal class ProductQueryConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasMany(p => p.OrderItems).WithOne(oi => oi.Product).HasForeignKey(oi => oi.ProductId).HasPrincipalKey(p => p.Id);
        builder.Property(p => p.StockAmount).HasColumnName("Stock_Amount");
        builder.Property(p => p.StockUnit).HasColumnName("Stock_UnitType");
        builder.OwnsMany(p => p.Prices, pricesBuilder =>
        {
            pricesBuilder.WithOwner().HasForeignKey("ProductId");
            pricesBuilder.ToTable("Prices");
        });


        builder.Property(p => p.StockUnit)
            .HasColumnName("Stock_UnitType")
            .HasConversion(
        s => s.ToString(),
        s => (UnitType)Enum.Parse(typeof(UnitType), s)
    );


    }
}