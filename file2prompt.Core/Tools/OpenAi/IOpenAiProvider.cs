namespace file2prompt.Core.Tools.OpenAi;

internal interface IOpenAiProvider
{
    Task<string?> SendMessage(
        string url,
        string apiKey,
        string model,
        string message);

    void Abort();
}