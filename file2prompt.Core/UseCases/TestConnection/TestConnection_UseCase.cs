using file2prompt.Core.External.OutputWriter;
using file2prompt.Core.Tools.OpenAi;
using file2prompt.Core.UseCases.Common;
using Microsoft.Extensions.Logging;

namespace file2prompt.Core.UseCases.TestConnection;

internal class TestConnection_UseCase(
    ILogger<TestConnection_UseCase> logger,
    IOpenAiProvider _openAiProvider) :
    BaseUseCase<TestConnection_Params, TestConnection_Result>(logger),
    ITestConnection_UseCase
{
    private IOutputWriter? _outputWriter;

    public void Setup(IOutputWriter outputWriter)
    {
        _outputWriter = outputWriter;
    }

    protected override async Task<TestConnection_Result> MainMethod(TestConnection_Params @params)
    {
        _outputWriter?.Clear();

        try
        {
            var response = await _openAiProvider.SendMessage(
                @params.Url,
                @params.ApiKey,
                @params.Model,
                "Hi! Who Are you?");

            _outputWriter?.WriteLine(response ?? "<Empty>");
        }
        catch (HttpRequestException ex)
        {
            _outputWriter?.WriteLine($"Failed to establish connection ({ex.StatusCode}): {ex.Message}");
        }
        catch (Exception ex)
        {
            _outputWriter?.WriteLine($"An unexpected error occurred: {ex.Message}");
            throw;
        }

        var result = new TestConnection_Result();
        return result;
    }
}
