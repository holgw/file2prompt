using file2prompt.Core.External.OutputWriter;
using file2prompt.Core.Tools.FileSystemManagement;
using file2prompt.Core.UseCases.Common;
using Microsoft.Extensions.Logging;

namespace file2prompt.Core.UseCases.TestFileSearch;

internal class TestFileSearch_UseCase(
    ILogger<TestFileSearch_UseCase> logger,
    IFileSystemManager _fileSysManager) :
    BaseUseCase<TestFileSearch_Params, TestFileSearch_Result>(logger),
    ITestFileSearch_UseCase
{
    private IOutputWriter? _outputWriter;

    public void Setup(IOutputWriter outputWriter)
    {
        _outputWriter = outputWriter;
    }

    protected override Task<TestFileSearch_Result> MainMethod(TestFileSearch_Params @params)
    {
        _outputWriter?.Clear();
        var files = _fileSysManager.Find(@params.WorkingDirectory!, regex: @params.Regex);
        _logger.LogInformation("Files detected: {filesLength}", files.Length);
        _outputWriter?.WriteLine($"Files detected: {files.Length}");

        int i = 1;
        foreach (var file in files)
        {
            _outputWriter?.WriteLine($"[{i}]: {file}");
            i++;
        }

        var result = new TestFileSearch_Result();
        return Task.FromResult(result);
    }
}
