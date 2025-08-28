using file2prompt.Core.Tools.OpenAi;
using file2prompt.Core.UseCases.Common;
using Microsoft.Extensions.Logging;

namespace file2prompt.Core.UseCases.AbortInference;

internal class AbortInference_UseCase(
    ILogger<AbortInference_UseCase> logger,
    IOpenAiProvider _openAiProvider) :
    BaseUseCase<AbortInference_Params, AbortInference_Result>(logger),
    IAbortInference_UseCase
{
    protected override Task<AbortInference_Result> MainMethod(
        AbortInference_Params @params)
    {
        _openAiProvider.Abort();
        var result = new AbortInference_Result();
        return Task.FromResult(result);
    }
}
