using Graphql.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Catalogue.Configurations;



internal class OrderItemQueryConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasOne(oi => oi.Order).WithMany(o => o.OrderItems).HasForeignKey(oi => oi.OrderId).HasPrincipalKey(o => o.Id);
        builder.HasOne(oi => oi.Product).WithMany(p => p.OrderItems).HasForeignKey(oi => oi.OrderId).HasPrincipalKey(p => p.Id);
    }
}