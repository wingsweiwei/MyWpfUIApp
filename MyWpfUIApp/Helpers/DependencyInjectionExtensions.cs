using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MyWpfUIApp.Helpers;

internal static class DependencyInjectionExtensions
{
    public static IServiceCollection AddTransientFromNamespace(this IServiceCollection services, string namespaceName, ServiceLifetime serviceLifetime)
    {
        var assembly = Assembly.GetEntryAssembly() ?? throw new InvalidOperationException("Entry assembly not found.");
        foreach (var type in assembly.GetTypes()
            .Where(x => x.IsClass && x.Namespace!.StartsWith(namespaceName, StringComparison.InvariantCultureIgnoreCase))
            .Where(type => services.All(x => x.ServiceType != type)))
        {
            switch (serviceLifetime)
            {
                case ServiceLifetime.Singleton: services.AddSingleton(type); break;
                case ServiceLifetime.Scoped: services.AddScoped(type); break;
                case ServiceLifetime.Transient: services.AddTransient(type); break;
                default: break;
            }
        }
        return services;
    }
}
