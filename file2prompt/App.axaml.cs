using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Templates;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using file2prompt.Core;
using file2prompt.Core.External.OutputWriter;
using file2prompt.ViewModels;
using file2prompt.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace file2prompt;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var services = new ServiceCollection();
        services.AddViewModels();
        var serviceProvider = services.BuildServiceProvider();

        var locator = new ViewLocator(serviceProvider);
        DataTemplates.Add(locator);

        // Line below is needed to remove Avalonia data validation.
        // Without this line you will get duplicate validations from both Avalonia and CT
        BindingPlugins.DataValidators.RemoveAt(0);

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = serviceProvider.GetRequiredService<MainWindowViewModel>(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}

public static class Extensions
{
    public static IServiceCollection AddViewModels(this ServiceCollection services)
    {
        services.AddSingleton<IOutputWriter, OutputWriter>();
        services.AddSingleton<IOutputViewModel, MainViewModel>();
        services.AddSingleton<MainViewModel>();

        services.AddSingleton<MainView>();
        services.AddSingleton<MainWindowViewModel>();
        services.AddSingleton<MainWindow>();

        services.AddCore();

        return services;
    }
}

internal class ViewLocator : IDataTemplate
{
    private readonly IServiceProvider _serviceProvider;
    private readonly Dictionary<Type, Func<Control?>> _locator = [];

    // CTOR
    public ViewLocator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        RegisterViewFactory<MainWindowViewModel, MainWindow>();
        RegisterViewFactory<MainViewModel, MainView>();
    }

    public Control Build(object? data)
    {
        if (data is null)
        {
            return new TextBlock { Text = "No VM provided" };
        }

        _locator.TryGetValue(data.GetType(), out var factory);

        return factory?.Invoke() ?? new TextBlock { Text = $"VM Not Registered: {data.GetType()}" };
    }

    public bool Match(object? data)
    {
        return data is not string;
    }

    private void RegisterViewFactory<TViewModel, TView>()
        where TViewModel : class
        where TView : Control
        => _locator.Add(
            typeof(TViewModel),
            Design.IsDesignMode
                ? Activator.CreateInstance<TView>
                : _serviceProvider.GetRequiredService<TView>);
}