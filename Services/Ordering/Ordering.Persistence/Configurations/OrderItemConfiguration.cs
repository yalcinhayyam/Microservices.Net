
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Persistence.Models;
using Ordering.Persistence.Models.Entities;

namespace Ordering.Persistence.Configurations;

internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(oi => oi.Id);


        builder.Property(o => o.Id).HasConversion(
            id => id.Value,
            id => new(id)
);
        builder.HasOne(oi => oi.Order).WithMany(p => p.Items).HasForeignKey(oi => oi.OrderId).HasPrincipalKey(o => o.Id);
    }
}