using file2prompt.Core.Tools.OpenAi.Entity;
using file2prompt.Core.UseCases.Common;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace file2prompt.Core.Tools.OpenAi;

internal class OpenAiProvider() : IOpenAiProvider
{
    private CancellationTokenSource? CancellationTokenSource { get; set; }

    public async Task<string?> SendMessage(
        string url,
        string apiKey,
        string model,
        string message)
    {
        using var client = new HttpClient() { Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite) };
        var data = new
        {
            messages = new[]
            {
                new
                {
                    role = "system",
                    content = Constants.SystempPrompt,
                },
                new
                {
                    role = "user",
                    content = message
                },
            },
            temperature = Constants.Temperature,
            max_tokens = Constants.MaxTokens,
            stream = false,
        };

        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

        this.CancellationTokenSource = new CancellationTokenSource();
        HttpResponseMessage response = await client.PostAsync(
            url,
            JsonContent.Create(data),
            this.CancellationTokenSource.Token);

        if (response.IsSuccessStatusCode)
        {
            var openAiResponse = await response.Content.ReadFromJsonAsync<OpenAiResponse>();
            return openAiResponse?.Choices?.FirstOrDefault()?.Message?.Content;
        }
        else
        {
            throw new HttpRequestException(
                message: response.ReasonPhrase,
                inner: null,
                statusCode: response.StatusCode);
        }
    }

    public void Abort()
    {
        this.CancellationTokenSource?.Cancel();
    }
}
