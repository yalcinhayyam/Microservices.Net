using Graphql.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Catalogue.Configurations;



internal class OrderQueryConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasMany(o => o.OrderItems).WithOne(oi => oi.Order).HasForeignKey(oi => oi.OrderId).HasPrincipalKey(o => o.Id);
        builder.Property(o => o.Status).HasConversion(
            s => s.ToString(),
            s => (OrderStatus)Enum.Parse(typeof(OrderStatus), s)
        );
        builder.HasKey(o => o.Id);


    }
}