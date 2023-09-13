
using Catalogue.Domain;
using Microsoft.EntityFrameworkCore;

namespace Catalogue.Application.Abstraction.Contexts;


public interface ICatalogueDbContext
{

    Task BeginTransactionAsync(CancellationToken cancellationToken);
    Task CommitTransactionAsync(CancellationToken cancellationToken);
    Task RollbackTransactionAsync(CancellationToken cancellationToken);
    DbSet<Product> Products { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

}