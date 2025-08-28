using file2prompt.Core.Tools.FileSystemManagement;
using file2prompt.Core.UseCases.Common;
using file2prompt.Core.UseCases.Common.Tools.PresetsManagement;
using file2prompt.Core.UseCases.Common.Tools.PromptsManagement;
using Microsoft.Extensions.Logging;

namespace file2prompt.Core.UseCases.GetAllSettings;

internal class GetAllSettings_UseCase(
    ILogger<GetAllSettings_UseCase> logger,
    IFileSystemManager _fileSystemManager,
    IPromptsManager _promptsManager,
    IPresetsManager _presetsManager) :
    BaseUseCase<GetAllSettings_Params, GetAllSettings_Result>(logger),
    IGetAllSettings_UseCase
{
    protected override Task<GetAllSettings_Result> MainMethod(
        GetAllSettings_Params @params)
    {
        var curDir = _fileSystemManager.GetCurrentDirectory();

        var promptsDirectory = Path.Combine(curDir, Constants.PromptsFolderName);
        var promptsDictionary = _promptsManager.LoadDictionary(promptsDirectory);

        var presetsDirectory = Path.Combine(curDir, Constants.PresetsFolderName);
        var presets = _presetsManager.LoadDictionary(presetsDirectory);

        var result = new GetAllSettings_Result()
        {
            PresetsDirectory = presetsDirectory,
            Presets = presets,
            PromptsDirectory = promptsDirectory,
            Prompts = promptsDictionary,
            OutputDirectory = Path.Combine(curDir, Constants.OutputFolderName),
        };

        return Task.FromResult(result);
    }
}
