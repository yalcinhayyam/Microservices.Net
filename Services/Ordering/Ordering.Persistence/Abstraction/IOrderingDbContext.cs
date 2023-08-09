using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Ordering.Persistence.Models;
using Ordering.Persistence.Models.Entities;

namespace Ordering.Persistence.Abstraction;

public interface IOrderingDbContext
{
    DatabaseFacade Database { get; }
    DbSet<Order> Orders { get; }
    DbSet<OrderItem> OrderItems { get; }
    int SaveChanges();

}