
using Catalogue.Application.Abstraction.Contexts;
using Catalogue.Application.Abstraction.Repositories;
using Catalogue.Domain;
using Catalogue.Persistence.Contexts;

namespace Catalogue.Persistence.Repositories;


public sealed class ProductRepository : IProductRepository
{
    private readonly ICatalogueDbContext context;
    public ProductRepository(ICatalogueDbContext context)
    {
        this.context = context;
    }
    public async Task Add(Product product, CancellationToken cancellationToken = default)
    {
        context.Products.Add(product);
        await context.SaveChangesAsync(cancellationToken);
    }
}