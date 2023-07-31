using MediatR;
using Core.Common.Services;
using Core.Common;
using Catalogue.Application.Abstraction.Contexts;
using Catalogue.Domain.ValueObjects;
using Contracts.Ordering.ValueObjects;

namespace Catalogue.Application.Features.Catalogue.Commands.UpdateStock;


public sealed record ProductWithIdNotFoundError(ProductId ProductId) : Error($"Product with Id {ProductId.Value} not found.");

public sealed record UpdateStockCommand(Guid OrderId,IReadOnlyCollection<OrderItem> OrderItems) : IRequest<Result<Unit>>
{

    public sealed class UpdateStockCommandHandler : IRequestHandler<UpdateStockCommand, Result<Unit>>
    {
        private readonly ICatalogueDbContext context;
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly IPublisher publisher;

        public UpdateStockCommandHandler(ICatalogueDbContext context, IDateTimeProvider dateTimeProvider, IPublisher publisher)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
            this.publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
        }

        public async Task<Result<Unit>> Handle(UpdateStockCommand request, CancellationToken cancellationToken)
        {

            await context.BeginTransactionAsync(cancellationToken);
            foreach (var item in request.OrderItems)
            {
                var product = await context.Products.FindAsync(item.ProductId);
                if (product is null)
                {
                    await context.RollbackTransactionAsync(cancellationToken);
                    return new ProductWithIdNotFoundError(new(item.ProductId));
                }

                product.RemoveStockByAmount(item.Amount);
            }

            await context.CommitTransactionAsync(cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }


}