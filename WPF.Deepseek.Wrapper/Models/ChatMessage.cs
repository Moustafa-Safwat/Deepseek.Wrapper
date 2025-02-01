namespace WPF.Deepseek.Wrapper.Models;
/// <summary>
/// Represents a chat message with content and a flag indicating if it is a user message.
/// </summary>
public partial class ChatMessage : ObservableObject
{
    /// <summary>
    /// Gets or sets the content of the chat message.
    /// </summary>
    [ObservableProperty]
    private string _content = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether the message is from a user.
    /// </summary>
    [ObservableProperty]
    private bool _isUserMessage;
}
