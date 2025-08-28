using file2prompt.Core.UseCases.Common;
using file2prompt.Core.UseCases.Common.Tools.PresetsManagement;
using Microsoft.Extensions.Logging;

namespace file2prompt.Core.UseCases.SavePreset;

internal class SavePreset_UseCase(
    ILogger<SavePreset_UseCase> logger,
    IPresetsManager _presetsManager) :
    BaseUseCase<SavePreset_Params, SavePreset_Result>(logger),
    ISavePreset_UseCase
{
    protected override Task<SavePreset_Result> MainMethod(SavePreset_Params @params)
    {
        _presetsManager.SavePreset(@params.PresetsDirectory, @params.Preset);
        var result = new SavePreset_Result();
        return Task.FromResult(result);
    }
}
