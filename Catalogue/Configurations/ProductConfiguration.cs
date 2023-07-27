using Catalogue.Contexts;
using Catalogue.Models;
using Catalogue.Models.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Catalogue.Configurations;



public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.ToTable(nameof(ApplicationDbContext.Products));

        builder.Property(p => p.Id).HasColumnName(nameof(Product.Id));

        builder.Property(p => p.Title)
               .IsRequired()
               .HasMaxLength(100).HasColumnName(nameof(Product.Title));


        builder.OwnsMany(p => p.Prices, pricesBuilder =>
        {
            pricesBuilder.ToTable("Money");

            pricesBuilder.WithOwner().HasForeignKey("ProductId");
            pricesBuilder.Property<Guid>("Id");
            pricesBuilder.HasKey("Id");

            pricesBuilder.Property<decimal>(p => p.Amount)
                        .HasColumnName(nameof(Money.Amount))
                        .IsRequired();

            pricesBuilder.Property<int>(p => (int)
                p.CurrencyType)
                         .HasColumnName(nameof(Money.CurrencyType))
                         .IsRequired();
        });

        builder.OwnsOne(p => p.Stock, stockBuilder =>
        {
            stockBuilder.Property(s => s.UnitType)
                .HasColumnName($"{nameof(Product.Stock)}_{nameof(ProductUnit.UnitType)}")
                .IsRequired()
                .HasConversion<string>();

            stockBuilder.Property(s => s.Amount)
                .HasColumnName($"{nameof(Product.Stock)}_{nameof(ProductUnit.Amount)}")
                .IsRequired();

        });
    }
}