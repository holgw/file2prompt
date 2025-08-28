namespace file2prompt.Core.UseCases.Common;

internal record LlmResponse(
    string SourceFilePath,
    string Body,
    string TargetFilePath,
    ExtractionTags? ExtractionTags);