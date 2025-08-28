namespace file2prompt.Core.UseCases.Common.Tools.PromptsManagement;

public interface IPromptsManager
{
    PromptsDictionary LoadDictionary(string promptsDir);
    PromptInfo? LoadPrompt(string promptsDir, string? promptName);
}