using Library.EventBus;
using Library.EventBus.Events;
using Rebus.Bus;
using Library.UnitOfWork;
using Catalogue.Contexts;
using Catalogue.Models;
using Microsoft.AspNetCore.Mvc;
using Catalogue.Mappings;
using MapsterMapper;

var builder = WebApplication.CreateBuilder(args);

{
    var services = builder.Services;
    services.RegisterUnitOfWork();
    services.AddDbContext<ApplicationDbContext>();
    services.Configure<EventBusOptions>(builder.Configuration.GetSection("EventBusOptions"));
    services.AddMapping();


    var configuration = builder.Configuration;

    services.RegisterRebus();
}

var app = builder.Build();


app.MapGet("/event", (IBus bus) =>
{
    bus.Publish(new ExampleIntegrationEvent { Value = "Hello World" });
    return "Event Sended";
});


app.MapPost("/products", (ApplicationDbContext context, IDateTimeProvider dateTimeProvider, IMapper mapper, [FromBody] ProductInputModel model) =>
{
    // var product = Product.Create(model.Title, model.Stock, dateTimeProvider.UtcNow);

    var product = mapper.Map<Product>(model);
    // model.Prices.ToList().ForEach(price => product.AddPrice(price));

    var result = context.Products.Add(product);
    context.SaveChanges();
    return mapper.Map<ProductPayloadModel>(result.Entity);
});


app.MapGet("/products", (ApplicationDbContext context, IMapper mapper) =>
{
    return context.Products.Select(product => mapper.Map<ProductPayloadModel>(product)).ToList();
});

app.Run();
