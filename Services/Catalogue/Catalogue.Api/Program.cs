using Microsoft.AspNetCore.Mvc;
using MapsterMapper;
using MediatR;
using Core.Common.Services;
using Catalogue.Persistence.Contexts;
using Core.EventBus;
using Contracts.Catalogue.Api.CreateProduct;
using Catalogue.Application.Features.Catalogue.Commands.CreateProduct;
using Catalogue.Application;
using Catalogue.Persistence;
using Rebus.Bus;
using Rebus.Config;
using Microsoft.Extensions.Options;
using Rebus.Routing.TypeBased;
using Contracts.Ordering;
using Microsoft.EntityFrameworkCore;
using Contracts.Ordering.Events;

var builder = WebApplication.CreateBuilder(args);

{
    var services = builder.Services;
    services.RegisterCoreServices();
    services.AddLogging();
    services.RegisterApplicationServices();
    services.RegisterPersistence();
    services.Configure<EventBusOptions>(builder.Configuration.GetSection("EventBusOptions"));
    services.AddRebus(
           (configurer, provider) =>
            {
                var (ConnectionString, QueueConnectionName) = provider.GetRequiredService<IOptions<EventBusOptions>>().Value;
                return configurer
                    .Logging(l => l.ColoredConsole())
                    .Transport(t => t.UseRabbitMq(ConnectionString, QueueConnectionName))
                    .Routing(r => r.TypeBased().MapAssemblyOf<OrderCreatedIntegrationEvent>(OrderingEventNames.OrderCreated));
            }, onCreated: async (IBus bus) =>
            {
                await bus.Advanced.Topics.Subscribe(OrderingEventNames.OrderCreated);
            }).AutoRegisterHandlersFromAssembly(typeof(ApplicationExtensions).Assembly);


    services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    }));
    var configuration = builder.Configuration;
}



var app = builder.Build();

using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<CatalogueDbContext>();
    context.Database.Migrate();
}

// app.UseMiddleware<ExceptionHandlingMiddleware>();


// app.MapGet("/event", (IBus bus) =>
// {
//     bus.Publish(new ExampleIntegrationEvent { Value = "Hello World" });
//     return "Event Sended";
// });


app.MapPost("/products",
        async (ISender madiator, IMapper mapper, [FromBody] CreateProductInput model) =>
        {

            var result = await madiator.Send(mapper.Map<CreateProductCommand>(model));
            if (!result.IsSuccess)
                return Results.BadRequest(result.Error);
            return Results.Json(result.Value);

        }
    );


app.MapGet("/products", (CatalogueDbContext context, IMapper mapper) =>
{
    return context.Products.Select(product => mapper.Map<CreateProductPayload>(product)).ToList();
});


app.MapGet("/user", (CatalogueDbContext context, IMapper mapper) =>
{

    context.Users.Add(new User { Email = "Demo Email" });
    context.SaveChanges();

    var UserResult = context.Users.ToList();
    var UserViewresult = context.UserViews.ToList();

    return new { UserResult, UserViewresult };
});

app.UseCors("MyPolicy");
app.Run();








