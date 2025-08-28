namespace file2prompt.Core.UseCases.Common;

internal class Constants
{
    public const string PresetsFolderName = ".presets";

    public const string OutputFolderName = ".output";

    public const string PromptsFolderName = ".prompts";

    public const string PromptContentTagName = "[BODY]";

    public const string SystempPrompt = "You are a helpful, smart, kind, and efficient AI assistant. " +
        "You always fulfill the user's requests to the best of your ability.";

    public const decimal Temperature = 0.7M;

    public const int MaxTokens = -1;
}