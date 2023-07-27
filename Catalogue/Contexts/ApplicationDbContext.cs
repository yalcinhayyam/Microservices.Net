using System.Reflection;
using Catalogue.Models;
using Library.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Catalogue.Contexts;


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

public class ApplicationDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserView> UserViews { get; set; } 


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

        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        // optionsBuilder.UseInMemoryDatabase("Microservices.Net");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<IDomainEvent>();
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<User>(u =>
        {
            u.ToTable("User");
            u.Property(e => e.Email).HasColumnName("Email");
            u.HasOne<UserView>().WithOne().HasForeignKey<UserView>(e => e.Id); // Specify the foreign key property

        });

        modelBuilder.Entity<UserView>(u =>
        {
            u.ToTable("User");
            u.Property(e => e.Email).HasColumnName("Email");
        });
    }
}