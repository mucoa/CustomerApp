using Carter;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerApp.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddCarter();
        return services;
    }
}
