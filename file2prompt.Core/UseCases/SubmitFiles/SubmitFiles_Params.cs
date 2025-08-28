using file2prompt.Core.UseCases.Common.Tools.PromptsManagement;

namespace file2prompt.Core.UseCases.SubmitFiles;

public record SubmitFiles_Params(
    string Url,
    string ApiKey,
    string Model,
    string OutputDirectory,
    string WorkingDirectory,
    string? Regex,
    string? ExcludeRegex,
    PromptInfo Prompt,
    string? StartTag,
    string? EndTag,
    string? OutputFilePostfix);
