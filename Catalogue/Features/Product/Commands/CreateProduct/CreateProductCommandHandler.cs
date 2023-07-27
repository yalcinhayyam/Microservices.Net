
using Library.UnitOfWork;
using MediatR;
using Catalogue.Contexts;
using MapsterMapper;
using Catalogue.Models.ValueObjects;
using Catalogue.Models;
using Catalogue.Models.Events;

public sealed record ProductResult(Guid Id, string Title, IReadOnlyCollection<Money> Prices, ProductUnit Stock);

public sealed record CreateProductCommand(string Title, ICollection<Money> Prices, ProductUnit Stock) : IRequest<Result<ProductResult>>
{

    public sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<ProductResult>>
    {
        private readonly ApplicationDbContext context;
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly IMapper mapper;
        private readonly IPublisher publisher;

        public CreateProductCommandHandler(ApplicationDbContext context, IDateTimeProvider dateTimeProvider, IMapper mapper, IPublisher publisher)
        {
            this.context = context;
            this.dateTimeProvider = dateTimeProvider;
            this.mapper = mapper;
            this.publisher = publisher;
        }


        public async Task<Result<ProductResult>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = mapper.Map<Product>(request);
            product.Created(dateTimeProvider.UtcNow);

            context.Products.Add(product);

            context.SaveChanges();
            await publisher.Publish(new ProductCreated(product.Id));
            return await Task.FromResult(mapper.Map<ProductResult>(product));

        }
    }

}