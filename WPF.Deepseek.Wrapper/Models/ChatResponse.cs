using System.Text.Json.Serialization;
using WPF.Deepseek.Wrapper.Services;

namespace WPF.Deepseek.Wrapper.Models;

/// <summary>
/// Represents a response from a chat service.
/// </summary>
public class ChatResponse
{
    /// <summary>
    /// Gets or sets the message contained in the chat response.
    /// </summary>
    [JsonPropertyName("message")]
    public Message Message { get; set; } = null!;
}
