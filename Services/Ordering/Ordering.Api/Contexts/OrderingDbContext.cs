using System.Reflection;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Ordering.Persistence.Abstraction;
using Ordering.Persistence.Models;
using Ordering.Persistence.Models.Entities;
using Ordering.Persistence.Models.Enums;
using Ordering.Persistence.Models.ValueObjects;

namespace Ordering.Contexts;


public class OrderingDbContext : DbContext, IOrderingDbContext
{
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    private readonly IConfiguration configuration;
    public OrderingDbContext(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("CommonConnection"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Order).Assembly);
        modelBuilder.Entity<Order>().Ignore(o => o.DomainEvents);
    }
}