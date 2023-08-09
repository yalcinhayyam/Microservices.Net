using System.Reflection;
using Catalogue.Application.Abstraction.Contexts;
using Catalogue.Domain;
using Core.Common.Domain;
using Core.Common.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace Catalogue.Persistence.Contexts;


public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
}

public class UserView
{
    public int Id { get; set; }
    public string Email { get; set; }
}


public class CatalogueDbContext : DbContext, ICatalogueDbContext
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<User> Users => Set<User>();
    public DbSet<UserView> UserViews => Set<UserView>();


    private readonly IConfiguration configuration;
    private readonly IDateTimeProvider dateTimeProvider;
    public CatalogueDbContext(IConfiguration configuration, IDateTimeProvider dateTimeProvider)
    {
        this.configuration = configuration;
        this.dateTimeProvider = dateTimeProvider;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();

        optionsBuilder.UseSqlServer(configuration.GetConnectionString("CommonConnection"));
        // optionsBuilder.UseInMemoryDatabase("Microservices.Net");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<IDomainEvent>();
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        // modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.Entity<User>(u =>
        {
            u.ToTable("User");
            u.Property(e => e.Email).HasColumnName("Email");
            u.HasOne<UserView>().WithOne().HasForeignKey<UserView>(e => e.Id);

        });

        modelBuilder.Entity<UserView>(u =>
        {
            u.ToTable("User");
            u.Property(e => e.Email).HasColumnName("Email");
        });
    }

    public Task BeginTransactionAsync(CancellationToken cancellationToken)
    {
        return Database.BeginTransactionAsync(cancellationToken);
    }


    public Task CommitTransactionAsync(CancellationToken cancellationToken)
    {
        return Database.CommitTransactionAsync(cancellationToken);
    }

    public Task RollbackTransactionAsync(CancellationToken cancellationToken)
    {
       return Database.RollbackTransactionAsync(cancellationToken);
    }
}