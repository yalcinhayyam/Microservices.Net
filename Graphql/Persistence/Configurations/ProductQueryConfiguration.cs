// using Catalogue.Contexts;
// using Catalogue.Models;
// using Catalogue.Models.Entities;
// using Catalogue.Models.ValueObjects;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
// namespace Catalogue.Configurations;



// public class ProductQueryConfiguration : IEntityTypeConfiguration<ProductQuery>
// {
//     public void Configure(EntityTypeBuilder<ProductQuery> builder)
//     {

//         // builder.OwnsOne(p => p.Stock, stockBuilder =>
//         //    {
//         //        stockBuilder.WithOwner();
//         //        stockBuilder.Property(p => p.Amount).HasColumnName("Stock_Amount");
//         //        stockBuilder.Property(p => p.UnitType).HasColumnName("Stock_UnitType");
//         //    }).Navigation(p => p.Stock).IsRequired();

//         builder.Property(p => p.Amount).HasColumnName("Stock_Amount");
//         builder.Property(p => p.UnitType).HasColumnName("Stock_UnitType");

//         builder.OwnsMany(p => p.Prices, pricesBuilder =>
//         {
//             pricesBuilder.WithOwner().HasForeignKey("ProductId");
//             pricesBuilder.ToTable("Prices");

//             // pricesBuilder.Property<Guid>("Id");
//             // pricesBuilder.HasKey("Id");
//         });

//     }
// }