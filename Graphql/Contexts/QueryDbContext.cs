using System.Reflection;
using Core.Common.Services;
using Graphql.Models;
using Microsoft.EntityFrameworkCore;

namespace Graphql.Contexts;


public interface IQueryDbContext
{
    DbSet<Product> Products { get; }
    DbSet<Order> Orders { get; }
    DbSet<OrderItem> OrderItems { get; }
    int SaveChanges();
}

public class QueryDbContext : DbContext, IQueryDbContext
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    private readonly IConfiguration configuration;
    private readonly IDateTimeProvider dateTimeProvider;
    public QueryDbContext(IConfiguration configuration, IDateTimeProvider dateTimeProvider)
    {
        this.configuration = configuration;
        this.dateTimeProvider = dateTimeProvider;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("CommonConnection"));
        // optionsBuilder.UseInMemoryDatabase("Catalogue");

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    }
}