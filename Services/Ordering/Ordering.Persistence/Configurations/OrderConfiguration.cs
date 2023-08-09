using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Ordering.Persistence.Models;
using Ordering.Persistence.Models.Entities;
using Ordering.Persistence.Models.Enums;
using Ordering.Persistence.Models.ValueObjects;

namespace Ordering.Persistence.Configurations;



internal class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {

        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).HasConversion(
                id => id.Value,
                id => new(id)
        );

        builder.HasMany(o => o.Items).WithOne(oi => oi.Order).HasForeignKey(oi => oi.OrderId).HasPrincipalKey(o => o.Id);


        builder.Property(o => o.Status)
         .HasConversion(orderStatus => orderStatus.Name,
              orderStatux => OrderStatus.FromName(orderStatux));

        builder
              .Property(o => o.OrderNumber)
              .HasConversion(number => number.Value,
                  number => new(number));

    }
}