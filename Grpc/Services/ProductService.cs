using Grpc.Core;
using Grpc.Protos;
namespace Grpc.Services;
internal class ProductService : Protos.Product.ProductBase
{

    public override Task<GetAllProductsReply> Products(GetAllProductsRequest request, ServerCallContext context)
    {
        var reply = new GetAllProductsReply() { Title = "Patates" };
        reply.Prices.AddRange(new Money[] { new () { Amount = 20, Currency = Currencies.Tl } });
        return Task.FromResult(reply);
    }
}