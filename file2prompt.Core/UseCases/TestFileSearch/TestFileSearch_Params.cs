namespace file2prompt.Core.UseCases.TestFileSearch;

internal record TestFileSearch_Params(
    string WorkingDirectory,
    string? Regex,
    string? ExcludeRegex);