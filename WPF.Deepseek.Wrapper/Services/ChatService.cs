using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using WPF.Deepseek.Wrapper.Models;

namespace WPF.Deepseek.Wrapper.Services;

/// <summary>
/// Service for handling chat messages with an AI model.
/// </summary>
public class ChatService
    (
    HttpClient client,
    IConfiguration configuration
    )
    : IChatService
{

    /// <summary>
    /// Sends a message asynchronously and returns a stream of responses.
    /// </summary>
    /// <param name="userMessage">The message to be sent by the user.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>An asynchronous stream of response messages.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when the URL key is not found in the configuration.</exception>
    public async IAsyncEnumerable<string> SendMessageAsync(string userMessage, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var requestData = new
        {
            model = configuration.GetValue<string>("AIModel"),
            messages = new[]
            {
                new
                {
                    role = configuration.GetValue<string>("Role"),
                    content = userMessage
                }
            }
        };

        string json = JsonSerializer.Serialize(requestData);
        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

        string? url = configuration.GetValue<string>("Url");
        if (url is null)
        {
            throw new KeyNotFoundException("Url key is not found in appsetting.json");
        }

        var request = new HttpRequestMessage(HttpMethod.Post, url) { Content = content };

        using HttpResponseMessage response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
        response.EnsureSuccessStatusCode();

        await using Stream responseStream = await response.Content.ReadAsStreamAsync(cancellationToken);
        using StreamReader reader = new(responseStream);

        while (!reader.EndOfStream && !cancellationToken.IsCancellationRequested)
        {
            string? line = await reader.ReadLineAsync(cancellationToken);
            if (!string.IsNullOrWhiteSpace(line))
            {
                var messageObject = JsonSerializer.Deserialize<ChatResponse>(line);
                if (messageObject?.Message?.Content != null)
                {
                    yield return messageObject.Message.Content; // Return chunk of text immediately
                }
            }
        }
    }
}
