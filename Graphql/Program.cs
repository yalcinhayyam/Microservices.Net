using Catalogue.Application.Features.Catalogue.Commands.CreateProduct;
using Contracts.Catalogue.Api.CreateProduct;
using Core.Common.Services;
using Graphql.Contexts;
using Graphql.Models;
using MapsterMapper;
using MediatR;
using Catalogue.Application;
using Catalogue.Persistence;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Enums;
using Shared.Common.ValueObjects;
using HotChocolate.Subscriptions;
using HotChocolate.Execution;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<IQueryDbContext, QueryDbContext>();
builder.Services.RegisterApplicationServices();
builder.Services.RegisterPersistence();
builder.Services.RegisterCoreServices();

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>()
    .AddInMemorySubscriptions()
    .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = builder.Environment.IsDevelopment())
      .AddProjections()
      .AddSorting()
      .AddFiltering();
//   .AddType<ResponseType<Product,Product>>();



builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
}


app.MapGraphQL();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseWebSockets();

app.MapPost("/", (IQueryDbContext context) =>
{
    var product = new Product { Title = "Patates", Id = Guid.NewGuid(), StockAmount = 1000, StockUnit = UnitType.Kg, CreatedAt = DateTime.Now, Prices = new List<MoneyModel> { new() { Amount = 100, CurrencyType = Currencies.TL } } };
    context.Products.Add(product);
    context.Orders.Add(new Order
    {
        Id = Guid.NewGuid(),
        CreatedAt = DateTime.Now,
        OrderNumber = "klwkldwq",
        OrderItems = new List<OrderItem>
        {
            new OrderItem{Id= Guid.NewGuid(),Quantity = 10, ProductId = product.Id,CreatedAt = DateTime.Now }
        }
    });
    context.SaveChanges();

});

app.MapGet("/", (IQueryDbContext context) =>
{
    return context.Products
    .Include(p => p.OrderItems).SelectMany(p => p.OrderItems.Select(oi => oi.Quantity));
    // return context.Orders.Select(o=> o.Status);//.Include(p => p.OrderItems);
});
app.Run();


public class Query
{
    [UseFirstOrDefault]
    [UseProjection]
    [UseFiltering]
    public Product? GetProduct(Guid Id, [Service] IQueryDbContext context) => context.Products.FirstOrDefault(p => p.Id == Id);

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Product> GetProducts([Service] IQueryDbContext context) => context.Products;

}
public class Mutation
{
    public async Task<CreateProductPayload> AddProduct(CreateProductInput imput, [Service] ISender madiator, [Service] IMapper mapper, [Service] ITopicEventSender sender)
    {
        var result = await madiator.Send(mapper.Map<CreateProductCommand>(imput));
        if (!result.IsSuccess)
            throw new Exception(result.Error.Message);
        await sender.SendAsync(nameof(Subscription.ProductCreated), result.Value.Id);
        return mapper.Map<CreateProductPayload>(result.Value);
    }

    public async Task<ChatMessage> SendMessage(string clientId, ChatMessage message, [Service] ITopicEventSender sender)
    {
        await sender.SendAsync(clientId, message);
        return message;
    }
}

public class Subscription
{
    [Subscribe]
    public Guid ProductCreated([EventMessage] Guid id) => id;

    [Subscribe]
    public async ValueTask<ISourceStream<ChatMessage>> SubscribeToChatMessages(
            string receiverId,
            [Service] ITopicEventReceiver receiver, CancellationToken cancellationToken)
    {
        return await receiver.SubscribeAsync<ChatMessage>(receiverId, cancellationToken);
    }

    [Subscribe(With = nameof(SubscribeToChatMessages))]
    public ChatMessage ChatMessageReceived([EventMessage] ChatMessage message)
    {
        return message;
    }
}

public class ChatMessage
{
    public string value { get; set; }
}


public class Response<T>
{
    public string Status { get; set; }

    public T Payload { get; set; }
}

public class ResponseType<TSchemaType, TRuntimeType>
    : ObjectType<Response<TRuntimeType>>
    where TSchemaType : class, IOutputType
{
    protected override void Configure(
        IObjectTypeDescriptor<Response<TRuntimeType>> descriptor)
    {
        descriptor.Field(f => f.Status);

        descriptor
            .Field(f => f.Payload)
            .Type<TSchemaType>();
    }
} 