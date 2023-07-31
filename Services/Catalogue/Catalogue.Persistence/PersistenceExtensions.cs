using Catalogue.Application.Abstraction.Contexts;
using Catalogue.Application.Abstraction.Repositories;
using Catalogue.Persistence.Contexts;
using Catalogue.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Catalogue.Persistence;



public static class PersistenceExtensions
{
    public static IServiceCollection RegisterPersistence(this IServiceCollection services)
    {
        services.AddDbContext<ICatalogueDbContext,CatalogueDbContext>();
        services.AddScoped<IProductRepository, ProductRepository>();
        return services;
    }
}