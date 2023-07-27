using Library.EventBus;
using Library.EventBus.Events;
using Rebus.Bus;
using Library.UnitOfWork;
using Catalogue.Contexts;
using Catalogue.Models;
using Microsoft.AspNetCore.Mvc;
using Catalogue.Mappings;
using MapsterMapper;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

{
    var services = builder.Services;
    services.RegisterUnitOfWork();
    services.AddDbContext<ApplicationDbContext>();
    services.Configure<EventBusOptions>(builder.Configuration.GetSection("EventBusOptions"));
    services.AddMapping();
    services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
    services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    }));
    var configuration = builder.Configuration;

    services.RegisterRebus();
}



var app = builder.Build();


app.MapGet("/event", (IBus bus) =>
{
    bus.Publish(new ExampleIntegrationEvent { Value = "Hello World" });
    return "Event Sended";
});


app.MapPost("/products",
        async (ISender madiator, IMapper mapper, [FromBody] ProductInputModel model) =>
        {

            var result = await madiator.Send(mapper.Map<CreateProductCommand>(model));
            if (!result.IsSuccess)
                return Results.BadRequest(result.Error);
            return Results.Json(result.Value);

        }
    );

app.MapGet("/products", (ApplicationDbContext context, IMapper mapper) =>
{
    return context.Products.Select(product => mapper.Map<ProductPayloadModel>(product)).ToList();
});

app.MapGet("/user", (ApplicationDbContext context, IMapper mapper) =>
{

    context.Users.Add(new User { Email = "Demo Email" });
    context.SaveChanges();

    var UserResult = context.Users.ToList();
    var UserViewresult = context.UserViews.ToList();

    return new { UserResult, UserViewresult };
});

app.UseCors("MyPolicy");
app.Run();


