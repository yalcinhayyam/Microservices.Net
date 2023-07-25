using System.Reflection;
using Catalogue.Models;
using Library.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Catalogue.Contexts;

public class ApplicationDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    private readonly IConfiguration configuration;
    private readonly IDateTimeProvider dateTimeProvider;
    public ApplicationDbContext(IConfiguration configuration, IDateTimeProvider dateTimeProvider)
    {
        this.configuration = configuration;
        this.dateTimeProvider = dateTimeProvider;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseInMemoryDatabase("Microservices.Net");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<IDomainEvent>();
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}