using file2prompt.Core.UseCases.SubmitFiles;

namespace file2prompt.Core.UseCases.Common;

internal record ExtractionTags(string? StartTag, string? EndTag)
{
    public bool IsEmpty()
    {
        return String.IsNullOrEmpty(StartTag) || String.IsNullOrEmpty(EndTag);
    }

    public static ExtractionTags? Build(SubmitFiles_Params options)
    {
        return new ExtractionTags(options.StartTag, options.EndTag);
    }
}