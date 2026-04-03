using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using FlowForge.Desktop.ViewModels;
using FlowForge.Desktop.Views;
using FlowForge.Engine;
using Microsoft.Extensions.DependencyInjection;

namespace FlowForge.Desktop;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            DisableAvaloniaDataAnnotationValidation();

            var services = new ServiceCollection();
            
            services.AddSingleton<IEngine, Engine.Engine>();
            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<MainWindow>(sp => new MainWindow
            {
                DataContext = sp.GetRequiredService<MainWindowViewModel>(),
            });

            var provider = services.BuildServiceProvider();

            desktop.Startup += async (_, _) => await InitializeEngineAsync(provider);
            desktop.MainWindow = provider.GetRequiredService<MainWindow>();
        }

        base.OnFrameworkInitializationCompleted();
    }

    private static async Task InitializeEngineAsync(IServiceProvider serviceProvider)
    {
        var engine = serviceProvider.GetRequiredService<IEngine>();
        await engine.InitializeAsync();
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
}