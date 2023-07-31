using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Ordering.Persistence.Models;

namespace Ordering.Persistence.Abstraction;

public interface IOrderingDbContext
{
    DatabaseFacade Database { get; }
    DbSet<Order> Orders { get; }
    int SaveChanges();

}