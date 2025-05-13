﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyWpfUIApp.Helpers;
using MyWpfUIApp.Services;
using MyWpfUIApp.ViewModels.Pages;
using MyWpfUIApp.ViewModels.Windows;
using MyWpfUIApp.Views.Pages;
using MyWpfUIApp.Views.Windows;
using System.IO;
using System.Windows.Threading;
using Wpf.Ui;
using Wpf.Ui.DependencyInjection;

namespace MyWpfUIApp;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    // The.NET Generic Host provides dependency injection, configuration, logging, and other services.
    // https://docs.microsoft.com/dotnet/core/extensions/generic-host
    // https://docs.microsoft.com/dotnet/core/extensions/dependency-injection
    // https://docs.microsoft.com/dotnet/core/extensions/configuration
    // https://docs.microsoft.com/dotnet/core/extensions/logging
    private static readonly IHost _host = Host
        .CreateDefaultBuilder()
        .ConfigureAppConfiguration(c =>
        {
            c.SetBasePath(Path.GetDirectoryName(AppContext.BaseDirectory) ?? throw new DirectoryNotFoundException("Unable to find the base directory of the application."));
        })
        .ConfigureServices((context, services) =>
        {
            services.AddNavigationViewPageProvider();

            services.AddHostedService<ApplicationHostService>();

            services.AddSingleton<IThemeService, ThemeService>();
            services.AddSingleton<ITaskBarService, TaskBarService>();
            services.AddSingleton<ISnackbarService, SnackbarService>();
            services.AddSingleton<IContentDialogService, ContentDialogService>();
            services.AddSingleton<INavigationService, NavigationService>();

            // Main window with navigation
            services.AddSingleton<INavigationWindow, MainWindow>();
            services.AddSingleton<MainWindowViewModel>();

            services.AddTransientFromNamespace("MyWpfUIApp.Views.Pages", ServiceLifetime.Transient);
            services.AddTransientFromNamespace("MyWpfUIApp.Views.Windows", ServiceLifetime.Transient);
            services.AddTransientFromNamespace("MyWpfUIApp.ViewModels.Pages", ServiceLifetime.Transient);
            services.AddTransientFromNamespace("MyWpfUIApp.ViewModels.Windows", ServiceLifetime.Transient);

            services.AddLocalization();
        }).Build();

    /// <summary>
    /// Gets services.
    /// </summary>
    public static IServiceProvider Services => _host.Services;

    /// <summary>
    /// Occurs when the application is loading.
    /// </summary>
    private async void OnStartup(object sender, StartupEventArgs e)
    {
        await _host.StartAsync();
    }

    /// <summary>
    /// Occurs when the application is closing.
    /// </summary>
    private async void OnExit(object sender, ExitEventArgs e)
    {
        await _host.StopAsync();

        _host.Dispose();
    }

    /// <summary>
    /// Occurs when an exception is thrown by an application but not handled.
    /// </summary>
    private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        // For more info see https://docs.microsoft.com/en-us/dotnet/api/system.windows.application.dispatcherunhandledexception?view=windowsdesktop-6.0
    }
}
