
using System.Reflection;
using Library.EventBus;
using Library.EventBus.EventHandlers;
using Library.EventBus.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Rebus.Bus;
using Rebus.Config;

public static class RebusEventBusExtensions
{

    public static IServiceCollection RegisterRebus(this IServiceCollection serviceDescriptors, Func<IBus, Task>? onCreated = null)
    {
        return serviceDescriptors.AddRebus(
                   (configurer, provider) =>
                   {
                       var (ConnectionString, QueueName) = provider.GetRequiredService<IOptions<EventBusOptions>>().Value;

                       return configurer
                           .Logging(l => l.Serilog())
                           .Transport(t => t.UseRabbitMq(ConnectionString, QueueName));
                       // .Routing(r => r.TypeBased().MapAssemblyOf<ExampleIntegrationEvent>(QueueName));
                   }, onCreated: async (IBus bus) =>
                   {
                       // await bus.Subscribe<ExampleIntegrationEvent>();
                       if (onCreated is not null)
                       {
                           await onCreated.Invoke(bus);
                       }
                   });
                   //.AutoRegisterHandlersFromAssembly(Assembly.GetExecutingAssembly());
                    // .AddRebusHandler<ExampleIntegrationEventHandler>();

    }

}