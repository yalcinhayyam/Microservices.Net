using System.Reflection;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Ordering.Persistence.Abstraction;
using Ordering.Persistence.Models;
using Ordering.Persistence.Models.Enums;
using Ordering.Persistence.Models.ValueObjects;

namespace Ordering.Contexts;


public class OrderingDbContext : DbContext, IOrderingDbContext
{
    public DbSet<Order> Orders => Set<Order>();

    private readonly IConfiguration configuration;
    public OrderingDbContext(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.Entity<Order>().Ignore(o => o.DomainEvents);


        var orderIdConverter = new ValueConverter<OrderId, Guid>(
                id => id.Value,
                id => new(id)
        );

        modelBuilder.Entity<Order>()
            .Property(o => o.Id)
            .HasConversion(orderIdConverter);

        modelBuilder.Entity<Order>()
                    .HasKey(o => o.Id);

        modelBuilder.Entity<Order>().OwnsMany(p => p.Items);

       var orderStatusConverter = new ValueConverter<OrderStatus, string>(
                orderStatus => orderStatus.Name,
                orderStatux => OrderStatus.FromName(orderStatux)
        );
        modelBuilder.Entity<Order>()
            .Property(o => o.Status)
            .HasConversion(orderStatusConverter);



        var orderNumberConverter = new ValueConverter<OrderNumber, string>(
                number => number.Value,
                number => new(number)
        );



        modelBuilder.Entity<Order>()
            .Property(o => o.OrderNumber)
            .HasConversion(orderNumberConverter);

    }
}