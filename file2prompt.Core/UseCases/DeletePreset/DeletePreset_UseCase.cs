using file2prompt.Core.UseCases.Common;
using file2prompt.Core.UseCases.Common.Tools.PresetsManagement;
using Microsoft.Extensions.Logging;

namespace file2prompt.Core.UseCases.DeletePreset;

internal class DeletePreset_UseCase(
    ILogger<DeletePreset_UseCase> logger,
    IPresetsManager _presetsManager) :
    BaseUseCase<DeletePreset_Params, DeletePreset_Result>(logger),
    IDeletePreset_UseCase
{
    protected override Task<DeletePreset_Result> MainMethod(DeletePreset_Params @params)
    {
        _presetsManager.DeletePreset(@params.PresetsDir, @params.PresetName);
        var result = new DeletePreset_Result();
        return Task.FromResult(result);
    }
}
