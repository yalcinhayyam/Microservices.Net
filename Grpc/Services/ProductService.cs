using Grpc.Core;

namespace Grpc.Services;
internal class ProductService : Product.ProductBase
{

    public override Task<GetAllProductsReply> Products(GetAllProductsRequest request, ServerCallContext context)
    {
        var reply = new GetAllProductsReply() { Title = "Patates" };
        reply.Prices.AddRange(new Money[] { new Money() { Amount = 20, Currency = Currencies.Tl } });
        return Task.FromResult(reply);
    }
}