namespace file2prompt.Core.UseCases.Common.Tools;

internal static class Extensions
{
    public static string InjectContent(this string? prompt, string content)
    {
        prompt = (prompt ?? string.Empty).Trim();

        if (string.IsNullOrEmpty(prompt))
            return content;

        if (!prompt.Contains(Constants.PromptContentTagName, StringComparison.OrdinalIgnoreCase))
            return $"{prompt}{Environment.NewLine}{content}";

        return prompt.Replace(Constants.PromptContentTagName, content, StringComparison.OrdinalIgnoreCase);
    }

    public static string ExtractContent(this string? source, string? startTag, string? endTag)
    {
        // Validate input
        if (string.IsNullOrEmpty(source))
        {
            return string.Empty;
        }

        // Find the starting index of the startTag
        int startIndex = 0;
        if (!string.IsNullOrEmpty(startTag))
        {
            startIndex = source.IndexOf(startTag, StringComparison.OrdinalIgnoreCase);
            if (startIndex == -1)
            {
                startIndex = 0;
            }
            else
            {
                startIndex += startTag.Length;
            }
        }

        // Find the ending index of the endTag
        int endIndex = source.Length;
        if (!string.IsNullOrEmpty(endTag))
        {
            endIndex = source.IndexOf(endTag, startIndex, StringComparison.OrdinalIgnoreCase);
            if (endIndex == -1)
            {
                endIndex = source.Length;
            }
        }

        // Extract the substring between the startTag and endTag
        return source[startIndex..endIndex];
    }
}
