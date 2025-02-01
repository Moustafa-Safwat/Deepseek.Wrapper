using System.Text.Json.Serialization;

namespace WPF.Deepseek.Wrapper.Models;

/// <summary>
/// Represents a message with content.
/// </summary>
public class Message
{
    /// <summary>
    /// Gets or sets the content of the message.
    /// </summary>
    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;
}
