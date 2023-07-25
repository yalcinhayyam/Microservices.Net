

using Microsoft.Extensions.DependencyInjection;

namespace Library.UnitOfWork;


public static class UnitOfWorkExtensions {
    public static IServiceCollection RegisterUnitOfWork( this IServiceCollection  serviceDescriptors) {
        return serviceDescriptors.AddSingleton<IDateTimeProvider, DateTimeProvider>();
    }
}