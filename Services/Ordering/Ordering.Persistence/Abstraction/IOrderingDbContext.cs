using Microsoft.EntityFrameworkCore;
using Ordering.Persistence.Models;

namespace Ordering.Persistence.Abstraction;

public interface IOrderingDbContext
{
    DbSet<Order> Orders { get; }
    int SaveChanges();

}