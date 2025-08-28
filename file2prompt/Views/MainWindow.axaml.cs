using Avalonia.Controls;
using System.Reflection;

namespace file2prompt.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        SetWindowTitleWithVersion();
    }

    private void SetWindowTitleWithVersion()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var version = assembly.GetName().Version!;
        this.Title = $"file2prompt v{version.Major}.{version.Minor}.{version.Build}";
    }
}
