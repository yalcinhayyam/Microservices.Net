
using Core.EventBus;
using Ordering.Contexts;
using Ordering.Persistence.Abstraction;
using Ordering.Persistence.Repositories;
using Rebus.Config;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using Ordering.Persistence.Models;
using Contracts.Ordering.ValueObjects;
using Core.Common.Services;
using Ordering.Persistence.Models.Enums;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

{
    var services = builder.Services;
    services.Configure<EventBusOptions>(builder.Configuration.GetSection("EventBusOptions"));
    services.AddSingleton<IOrderingDbContext, OrderingDbContext>();
    services.AddSingleton<IOrderRepository, OrderRepository>();
    services.RegisterCoreServices();
    var configuration = builder.Configuration;

    services.AddRebus(
        (configurer, provider) =>
        {
            var (ConnectionString, QueueConnectionName) = provider.GetRequiredService<IOptions<EventBusOptions>>().Value;
            return configurer
                    .Logging(l => l.ColoredConsole())
                    .Transport(t => t.UseRabbitMq(ConnectionString, QueueConnectionName));
        });
}

var app = builder.Build();

using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<IOrderingDbContext>();
    context.Database.Migrate();
}


app.MapGet("/create-order", (IOrderRepository repository, IDateTimeProvider dateTimeProvider, [FromBody] CreateOrderInput input) =>
{
    var order = Order.Create(input.OrderItems.ToList(), OrderStatus.Paid, dateTimeProvider.UtcNow);
    if (order.IsSuccess is not true)
    {
        return Results.BadRequest(order.Error);
    }
    var result = order.Value;
    repository.Create(result);

    return Results.Ok(new CreateOrderPayload(result.OrderNumber.Value, result.Items, result.Status.Name, result.CreatedAt));
});



app.Run();


public sealed record CreateOrderInput(IReadOnlyCollection<OrderItem> OrderItems);
public sealed record CreateOrderPayload(string OrderNumber, IReadOnlyCollection<OrderItem> OrderItems, string status, DateTime createdAt);