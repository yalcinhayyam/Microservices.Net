using Microsoft.Extensions.DependencyInjection;

namespace Catalogue.Application;


public static class ApplicationExtensions
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
        // services.AddTransient<ExceptionHandlingMiddleware>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        return services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationExtensions).Assembly));
    }

}