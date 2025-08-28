using file2prompt.Core.External.OutputWriter;
using file2prompt.Core.UseCases.AbortInference;
using file2prompt.Core.UseCases.Common.Tools.PresetsManagement;
using file2prompt.Core.UseCases.Common.Tools.PromptsManagement;
using file2prompt.Core.UseCases.DeletePreset;
using file2prompt.Core.UseCases.GetAllSettings;
using file2prompt.Core.UseCases.OpenFolder;
using file2prompt.Core.UseCases.SavePreset;
using file2prompt.Core.UseCases.SubmitFiles;
using file2prompt.Core.UseCases.TestConnection;
using file2prompt.Core.UseCases.TestFileSearch;

namespace file2prompt.Core.Services;

internal class Service(
    IGetAllSettings_UseCase getAllSettings_UseCase,
    IOpenFolder_UseCase openFolder,
    ISubmitFiles_UseCase submitFiles,
    IAbortInference_UseCase abortInference,
    ISavePreset_UseCase savePreset,
    IDeletePreset_UseCase deletePreset,
    ITestFileSearch_UseCase testFileSearch,
    ITestConnection_UseCase testConnection) : IService
{
    private IOutputWriter? OutputWriter { get; set; }

    public void SetupOutputWriter(IOutputWriter outputWriter)
    {
        this.OutputWriter = outputWriter;
        submitFiles.Setup(outputWriter);
        testFileSearch.Setup(outputWriter);
        testConnection.Setup(outputWriter);
    }

    public Task<GetAllSettings_Result> GetAllSettings()
    {
        var @params = new GetAllSettings_Params();
        return getAllSettings_UseCase.Execute(@params);
    }

    public Task TestConnection(string? url, string? apiKey, string? model)
    {
        bool isValid = this.ValidateString("Url", url) &
             this.ValidateString("ApiKey", apiKey) &
             this.ValidateString("Model", model);

        if (!isValid)
            return Task.CompletedTask;

        var @params = new TestConnection_Params(url!, apiKey!, model!);
        return testConnection.Execute(@params);
    }

    public Task OpenFolder(string? path)
    {
        var @params = new OpenFolder_Params(path);
        return openFolder.Execute(@params);
    }

    public Task TestFileSearch(
        string? workingDirectory,
        string? regex,
        string? excludeRegex)
    {
        this.ValidateString("Working Directory", workingDirectory);

        var @params = new TestFileSearch_Params(
            workingDirectory!,
            regex,
            excludeRegex);

        return testFileSearch.Execute(@params);
    }

    public Task SubmitFiles(
        string? url,
        string? apiKey,
        string? model,
        string? outputDirectory,
        string? workingDirectory,
        string? regex,
        PromptInfo? prompt,
        string? startTag,
        string? endTag,
        string? outputFilePostfix)
    {
        bool isValid = this.ValidateString("Url", url) &
             this.ValidateString("ApiKey", apiKey) &
             this.ValidateString("Model", model) &
             this.ValidateString("Output Directory", outputDirectory) &
             this.ValidateString("Working Directory", workingDirectory) &
             this.ValidateString("Regex", regex) &
             this.ValidatePrompt("Prompt", prompt);

        if (!isValid)
            return Task.CompletedTask;

        var @params = new SubmitFiles_Params(
            url!,
            apiKey!,
            model!,
            outputDirectory!,
            workingDirectory!,
            regex,
            null,
            prompt!,
            startTag,
            endTag,
            outputFilePostfix);

        return submitFiles.Execute(@params);
    }

    public Task AbortInference()
    {
        var @params = new AbortInference_Params();
        return abortInference.Execute(@params);
    }

    public Task SavePreset(string presetsDirectory, Preset preset)
    {
        var @params = new SavePreset_Params(presetsDirectory, preset);
        return savePreset.Execute(@params);
    }

    public Task DeletePreset(string presetsDirectory, string? presetName)
    {
        bool isValid = this.ValidateString("Presets Directory", presetsDirectory) &
            this.ValidateString("Preset", presetName);

        if (!isValid)
            return Task.CompletedTask;

        var @params = new DeletePreset_Params(presetsDirectory, presetName!);
        return deletePreset.Execute(@params);
    }

    private bool ValidateString(string name, string? inputValue)
    {
        if (String.IsNullOrEmpty(inputValue))
        {
            this.OutputWriter?.WriteLine($" The \"{name}\" field is required. Please complete this field to continue.");
            return false;
        }

        return true;
    }

    private bool ValidatePrompt(string name, PromptInfo? prompt)
    {
        if (prompt is null)
        {
            this.OutputWriter?.WriteLine($" The \"{name}\" field is required. Please complete this field to continue.");
            return false;
        }

        return true;
    }
}
