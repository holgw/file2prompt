using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using file2prompt.Core.External.OutputWriter;
using file2prompt.Core.Services;
using file2prompt.Core.UseCases.Common.Tools.PresetsManagement;
using file2prompt.Core.UseCases.Common.Tools.PromptsManagement;
using file2prompt.Utility;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace file2prompt.ViewModels;

public partial class MainViewModel : ViewModelBase, IOutputViewModel
{
    private readonly IOutputWriter _outputWriter;
    private readonly IService _service;

    // PRESETS
    private string PresetsDirectory { get; }
    public RangeObservableCollection<Preset> Presets { get; } = [];
    [ObservableProperty] private Preset? selectedPreset;
    [ObservableProperty] private bool isNewPresetNameInputVisible;
    [ObservableProperty] private string? newPresetName;

    // CONNECTION
    [ObservableProperty] private string? url;
    [ObservableProperty] private string? apiKey;
    [ObservableProperty] private string? model;

    // PROCESSING
    [ObservableProperty] private string? workingDirectory;
    [ObservableProperty] private string? regex;
    private string PromptsDirectory { get; }
    [ObservableProperty] private PromptInfo? selectedPrompt;
    public RangeObservableCollection<PromptInfo> Prompts { get; } = [];

    // POST-PROCESSING
    [ObservableProperty] private string? startTag;
    [ObservableProperty] private string? endTag;
    [ObservableProperty] private string? outputFilePostfix;

    [ObservableProperty] private string? outputDirectory;
    [ObservableProperty] private string output;
    [ObservableProperty] private int progress;

    [ObservableProperty] private bool isProcessingNow;

    // CTOR
    public MainViewModel(IService service)
    {
        _service = service;
        _outputWriter = new OutputWriter(this);
        _service.SetupOutputWriter(_outputWriter);
        var settings = service.GetAllSettings().Result;

        this.PromptsDirectory = settings.PromptsDirectory ?? string.Empty;
        var prompts = settings.Prompts?.GetAll() ?? [];
        this.Prompts.AddRange(prompts);

        this.PresetsDirectory = settings.PresetsDirectory ?? string.Empty;
        var presets = settings.Presets?.GetAll() ?? [];
        this.Presets.AddRange(presets);
        this.SelectDefaultPreset();
        this.ApplyPreset(this.SelectedPreset);
        this.IsNewPresetNameInputVisible = false;

        this.OutputDirectory = settings.OutputDirectory;
        this.Output = string.Empty;
    }

    [RelayCommand]
    public async Task SavePresetCommand()
    {
        if (this.SelectedPreset is null)
            return;

        this.IsNewPresetNameInputVisible = false;

        this.SelectedPreset.Url = this.Url;
        this.SelectedPreset.ApiKey = this.ApiKey;
        this.SelectedPreset.Model = this.Model;
        this.SelectedPreset.WorkingDirectory = this.WorkingDirectory;
        this.SelectedPreset.Regex = this.Regex;
        this.SelectedPreset.PromptName = this.SelectedPrompt?.Name;
        this.SelectedPreset.StartTag = this.StartTag;
        this.SelectedPreset.EndTag = this.EndTag;
        this.SelectedPreset.OutputFilePostfix = this.OutputFilePostfix;

        await _service.SavePreset(this.PresetsDirectory, this.SelectedPreset);
    }

    [RelayCommand]
    public Task ShowNewPresetNameInput()
    {
        this.IsNewPresetNameInputVisible = true;
        return Task.CompletedTask;
    }

    [RelayCommand]
    public async Task SaveNewPreset()
    {
        if (string.IsNullOrEmpty(this.NewPresetName))
            return;

        var newPreset = new Preset()
        {
            Name = this.NewPresetName,
            Url = this.Url,
            ApiKey = this.ApiKey,
            Model = this.Model,
            WorkingDirectory = this.WorkingDirectory,
            Regex = this.Regex,
            PromptName = this.SelectedPrompt?.Name,
            StartTag = this.StartTag,
            EndTag = this.EndTag,
            OutputFilePostfix = this.OutputFilePostfix,
        };

        await _service.SavePreset(this.PresetsDirectory, newPreset);
        this.Presets.Add(newPreset);

        this.IsNewPresetNameInputVisible = false;
        this.NewPresetName = string.Empty;
    }

    [RelayCommand]
    public Task CancelSaveNewPreset()
    {
        this.IsNewPresetNameInputVisible = false;
        return Task.CompletedTask;
    }

    [RelayCommand]
    public async Task DeletePreset()
    {
        await _service.DeletePreset(
            this.PresetsDirectory,
            this.SelectedPreset?.Name);

        if (this.SelectedPreset is not null)
            this.Presets.Remove(this.SelectedPreset);

        this.SelectDefaultPreset();
    }

    [RelayCommand]
    public async Task StartProcessing()
    {
        this.IsProcessingNow = true;

        await Task.Run(async () =>
        {
            await _service.SubmitFiles(
                this.Url,
                this.ApiKey,
                this.Model,
                this.OutputDirectory,
                this.WorkingDirectory,
                this.Regex,
                this.SelectedPrompt,
                this.StartTag,
                this.EndTag,
                this.OutputFilePostfix);
        });

        this.IsProcessingNow = false;
    }

    [RelayCommand]
    public Task TestConnection()
    {
        return _service.TestConnection(
            this.Url,
            this.ApiKey,
            this.Model);
    }

    [RelayCommand]
    public Task AbortInference()
    {
        return _service.AbortInference();
    }

    [RelayCommand]
    public Task OpenFolder()
    {
        _service.OpenFolder(this.WorkingDirectory ?? string.Empty);
        return Task.CompletedTask;
    }

    [RelayCommand]
    public Task TestFileSearch()
    {
        return _service.TestFileSearch(this.WorkingDirectory, this.Regex, null);
    }

    [RelayCommand]
    public Task OpenTemplatesFolder()
    {
        _service.OpenFolder(this.PromptsDirectory);
        return Task.CompletedTask;
    }

    [RelayCommand]
    public Task OpenOutputFolder()
    {
        _service.OpenFolder(this.OutputDirectory);
        return Task.CompletedTask;
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);

        if (e.PropertyName == nameof(SelectedPreset))
        {
            this.ApplyPreset(this.SelectedPreset);
        }
    }

    private void ApplyPreset(Preset? preset)
    {
        this.Url = preset?.Url;
        this.ApiKey = preset?.ApiKey;
        this.Model = preset?.Model;
        this.WorkingDirectory = preset?.WorkingDirectory;
        this.Regex = preset?.Regex;
        this.SelectedPrompt = this.Prompts.FirstOrDefault(x => x.Name == this.SelectedPreset?.PromptName);
        this.StartTag = preset?.StartTag;
        this.EndTag = preset?.EndTag;
        this.OutputFilePostfix = preset?.OutputFilePostfix;
    }

    private void SelectDefaultPreset()
    {
        var selectedPreset = this.Presets.FirstOrDefault() ?? new Preset() { Name = "default" };

        if (!this.Presets.Any())
        {
            this.Presets.Add(selectedPreset);
        }

        this.SelectedPreset = selectedPreset;
    }
}
