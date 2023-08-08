using System.Reflection;
using Core.Common.Services;
using Graphql.Models;
using Microsoft.EntityFrameworkCore;

// namespace Catalogue.Contexts;

public class QueryDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

//     private readonly IConfiguration configuration;
//     private readonly IDateTimeProvider dateTimeProvider;
//     public QueryDbContext(IConfiguration configuration, IDateTimeProvider dateTimeProvider)
//     {
//         this.configuration = configuration;
//         this.dateTimeProvider = dateTimeProvider;
//     }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
    }

//     protected override void OnModelCreating(ModelBuilder modelBuilder)
//     {
//         modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
//         // modelBuilder.ApplyConfiguration(new ProductQueryConfiguration());


//     }
}