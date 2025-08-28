namespace file2prompt.ViewModels;

public partial class MainWindowViewModel(MainViewModel mainViewModel) :
    ViewModelBase
{
    public object? CurrentPage { get; set; } = mainViewModel;
}