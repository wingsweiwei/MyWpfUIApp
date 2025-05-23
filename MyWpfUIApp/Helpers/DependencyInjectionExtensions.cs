using Microsoft.Extensions.DependencyInjection;
using MyWpfUIApp.Services;
using MyWpfUIApp.ViewModels.Windows;
using MyWpfUIApp.Views.Windows;
using System.Reflection;

namespace MyWpfUIApp.Helpers;

internal static class DependencyInjectionExtensions
{
    private static readonly HashSet<Type> _excludedTypes =
    [
        typeof(ApplicationHostService),
        typeof(MainWindow),
        typeof(MainWindowViewModel)
    ];

    public static IServiceCollection AddFromNamespace(this IServiceCollection services, string namespaceName, ServiceLifetime serviceLifetime)
    {
        var assembly = Assembly.GetEntryAssembly() ?? throw new InvalidOperationException("Entry assembly not found.");
        foreach (var type in assembly.GetTypes()
            .Where(type => type.IsClass && type.Namespace!.StartsWith(namespaceName, StringComparison.InvariantCultureIgnoreCase))
            .Where(type => services.All(s => s.ServiceType != type))
            .Where(type => !_excludedTypes.Contains(type)))
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
