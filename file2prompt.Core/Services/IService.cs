using file2prompt.Core.External.OutputWriter;
using file2prompt.Core.UseCases.Common.Tools.PresetsManagement;
using file2prompt.Core.UseCases.Common.Tools.PromptsManagement;
using file2prompt.Core.UseCases.GetAllSettings;

namespace file2prompt.Core.Services;

public interface IService
{
    void SetupOutputWriter(IOutputWriter outputWriter);

    Task<GetAllSettings_Result> GetAllSettings();

    Task OpenFolder(string? path);

    Task TestConnection(
        string? url,
        string? apiKey,
        string? model);

    Task TestFileSearch(
        string? workingDirectory,
        string? regex,
        string? excludeRegex);

    Task SubmitFiles(
        string? url,
        string? apiKey,
        string? model,
        string? outputDirectory,
        string? workingDirectory,
        string? regex,
        PromptInfo? prompt,
        string? startTag,
        string? endTag,
        string? outputFilePostfix);

    Task AbortInference();
    Task SavePreset(string presetsDirectory, Preset preset);
    Task DeletePreset(string presetsDirectory, string? presetName);
}