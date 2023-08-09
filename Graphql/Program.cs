using Catalogue.Application.Features.Catalogue.Commands.CreateProduct;
using Contracts.Catalogue.Api.CreateProduct;
using Contracts.Catalogue.Enums;
using Core.Common.Services;
using Graphql.Contexts;
using Graphql.Models;
using MapsterMapper;
using MediatR;
using Catalogue.Application;
using Catalogue.Persistence;
using Microsoft.EntityFrameworkCore;
using Contracts.Shared.ValueObjects;
using Shared.Common.Enums;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<IQueryDbContext, QueryDbContext>();
builder.Services.RegisterApplicationServices();
builder.Services.RegisterPersistence();
builder.Services.RegisterCoreServices();

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = builder.Environment.IsDevelopment())
      .AddProjections()
      .AddSorting()
      .AddFiltering();




builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
}


app.MapGraphQL();
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapPost("/", (IQueryDbContext context) =>
{
    var product = new Product { Title = "Patates", Id = Guid.NewGuid(), StockAmount = 1000, StockUnit = UnitType.Kg, CreatedAt = DateTime.Now, Prices = new List<Money> { new() { Amount = 100, CurrencyType = Currencies.TL } } };
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
    public async Task<ProductPayloadModel> AddBook(ProductInputModel model, [Service] ISender madiator, [Service] IMapper mapper)
    {
        var result = await madiator.Send(mapper.Map<CreateProductCommand>(model));
        if (!result.IsSuccess)
            throw new Exception(result.Error.Message);
        return mapper.Map<ProductPayloadModel>(result.Value);
    }
}
