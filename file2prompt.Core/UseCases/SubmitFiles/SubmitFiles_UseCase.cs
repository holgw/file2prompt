using file2prompt.Core.External.OutputWriter;
using file2prompt.Core.Tools.FileSystemManagement;
using file2prompt.Core.Tools.OpenAi;
using file2prompt.Core.UseCases.Common;
using file2prompt.Core.UseCases.Common.Tools;
using file2prompt.Core.UseCases.Common.Tools.ResponsePrinting;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace file2prompt.Core.UseCases.SubmitFiles;

internal class SubmitFiles_UseCase(
    ILogger<SubmitFiles_UseCase> logger,
    IFileSystemManager _fileSysManager,
    IOpenAiProvider _openAiProvider,
    IReponsePrinter _reponsePrinter) :
    BaseUseCase<SubmitFiles_Params, SubmitFiles_Result>(logger),
    ISubmitFiles_UseCase
{
    private IOutputWriter? _outputWriter;

    public void Setup(IOutputWriter outputWriter)
    {
        _outputWriter = outputWriter;
    }

    protected override async Task<SubmitFiles_Result> MainMethod(SubmitFiles_Params @params)
    {
        ValidateInput(@params);

        _outputWriter?.Clear();
        _outputWriter?.WriteLine("Processing has begun");

        var files = _fileSysManager.Find(@params.WorkingDirectory!, regex: @params.Regex);
        _logger.LogInformation("Files detected: {filesLength}", files.Length);
        _outputWriter?.WriteLine($"Files detected: {files.Length}");

        string processingFolder = Path.Combine(
            _fileSysManager.GetCurrentDirectory(),
            @params.OutputDirectory!,
            $"{DateTime.Now:HH-mm-ss_dd-MM-yy} [{@params.Prompt.Name}]");

        _fileSysManager.CreateDirectory(processingFolder);

        var extractionTags = ExtractionTags.Build(@params);

        int c = 0;
        int step = 100 / files.Length;
        foreach (string sourceFilePath in files)
        {
            string partialFilePath = sourceFilePath.Replace(@params.WorkingDirectory!, "");

            _outputWriter?.WriteLine($"Let's start processing the file ({c + 1}\\{files.Length}): {partialFilePath}");

            if (!string.IsNullOrEmpty(@params.ExcludeRegex))
            {
                bool exclude = Regex.IsMatch(
                    Path.GetFileName(sourceFilePath),
                    @params.ExcludeRegex);

                if (exclude)
                {
                    _outputWriter?.WriteLine("The file is excluded from processing. Let's move on to the next one.");
                    continue;
                }
            }

            string filePathForLog = GetFilePathForLog(@params.WorkingDirectory!, sourceFilePath);

            _logger.LogInformation(
                "[{c}\\{filesLength}] Start processing file: {filePath}",
                c + 1,
                files.Length,
                filePathForLog);

            string fileContent = _fileSysManager.ReadAllText(sourceFilePath);
            string promptBody = @params.Prompt?.Body ?? string.Empty;
            string message = promptBody.InjectContent(fileContent);

            string? result = string.Empty;
            try
            {
                result = await _openAiProvider.SendMessage(
                    @params.Url,
                    @params.ApiKey,
                    @params.Model,
                    message);
            }
            catch (HttpRequestException ex)
            {
                _outputWriter?.WriteLine($" CONNECTION ERROR! ({(int)(ex.StatusCode ?? 0)}) {ex.Message}");
                throw;
            }

            _outputWriter?.SеtProgress(step);

            if (result is not null)
            {
                string partialFolder = new FileInfo(sourceFilePath)
                    .Directory!
                    .FullName
                    .Replace(@params.WorkingDirectory!, "")
                    .Trim('\\');

                string fileName = Path.GetFileNameWithoutExtension(partialFilePath);

                string outputFullPath = Path.Combine(
                    processingFolder,
                    partialFolder,
                    $"{fileName}{@params.OutputFilePostfix}");

                var llmResponse = new LlmResponse(
                    sourceFilePath,
                    result,
                    outputFullPath,
                    extractionTags);

                _reponsePrinter.Print(llmResponse);

                _logger.LogInformation("File processing is done!");
                c++;
            }
            else
            {
                _logger.LogInformation("Generation failed with empty result");
            }
        }

        _logger.LogInformation("Number of successfully processed files: {c}", c);
        _outputWriter?.WriteLine($"Processing complete! Number of successfully processed files: {c}");
        _outputWriter?.SеtProgress(100);

        return new SubmitFiles_Result();
    }

    private static string GetFilePathForLog(string root, string fullFilePath)
    {
        int index = fullFilePath.IndexOf(root);

        return index < 0
            ? fullFilePath
            : fullFilePath.Remove(index, root.Length);
    }

    private static void ValidateInput(SubmitFiles_Params @params)
    {
        if (string.IsNullOrEmpty(@params.OutputDirectory))
            throw new ArgumentException(nameof(@params.OutputDirectory));
    }
}