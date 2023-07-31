

using Microsoft.Extensions.DependencyInjection;

namespace Core.Common.Services;


public static class ServicesExtensions {
    public static IServiceCollection RegisterCoreServices( this IServiceCollection  serviceDescriptors) {
        return serviceDescriptors.AddSingleton<IDateTimeProvider, DateTimeProvider>();
    }
}