using file2prompt.Core.Tools.FileSystemManagement;
using file2prompt.Core.UseCases.Common;
using Microsoft.Extensions.Logging;

namespace file2prompt.Core.UseCases.OpenFolder;

internal class OpenFolder_UseCase(
    ILogger<OpenFolder_UseCase> logger,
    IFileSystemManager _fileSystemManager) :
    BaseUseCase<OpenFolder_Params, OpenFolder_Result>(logger),
    IOpenFolder_UseCase
{
    protected override Task<OpenFolder_Result> MainMethod(OpenFolder_Params @params)
    {
        _fileSystemManager.OpenFileOrFolder(@params.Path ?? String.Empty);
        var result = new OpenFolder_Result();
        return Task.FromResult(result);
    }
}