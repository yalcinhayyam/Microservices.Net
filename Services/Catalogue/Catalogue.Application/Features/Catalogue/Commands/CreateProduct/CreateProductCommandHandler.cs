using MediatR;
using MapsterMapper;
using Core.Common.Services;

using Core.Common;
using Catalogue.Domain;
using Catalogue.Domain.Events;
using Shared.Common.ValueObjects;
using Catalogue.Application.Abstraction.Repositories;
using Catalogue.Domain.ValueObjects;

namespace Catalogue.Application.Features.Catalogue.Commands.CreateProduct;

public sealed record ProductResult(Guid Id, string Title, IReadOnlyCollection<Money> Prices, ProductUnit Stock);

public sealed record CreateProductCommand(string Title, ICollection<Money> Prices, ProductUnit Stock) : IRequest<Result<ProductResult>>
{

    public sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<ProductResult>>
    {
        private readonly IProductRepository repository;
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly IMapper mapper;
        private readonly IPublisher publisher;

        public CreateProductCommandHandler(IProductRepository repository, IDateTimeProvider dateTimeProvider, IMapper mapper, IPublisher publisher)
        {
            this.repository = repository;
            this.dateTimeProvider = dateTimeProvider;
            this.mapper = mapper;
            this.publisher = publisher;
        }


        public async Task<Result<ProductResult>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = mapper.Map<Product>(request);
            product.Created(dateTimeProvider.UtcNow);

            await repository.Add(product);
            await publisher.Publish(new ProductCreated(product.Id));
            return await Task.FromResult(mapper.Map<ProductResult>(product));

        }
    }

}