using Microsoft.Extensions.DependencyInjection;

namespace file2prompt.Core.Services;

internal static class DependencyInjectionExtensions
{
    public static IServiceCollection AddServices(this ServiceCollection services)
    {
        services.AddSingleton<IService, Service>();
        return services;
    }
}