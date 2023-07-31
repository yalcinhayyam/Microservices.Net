



using Catalogue.Domain;

namespace Catalogue.Application.Abstraction.Repositories;


public interface IProductRepository
{
    Task Add(Product product, CancellationToken cancellationToken = default);

}