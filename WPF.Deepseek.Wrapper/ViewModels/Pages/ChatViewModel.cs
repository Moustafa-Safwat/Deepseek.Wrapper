using System.Collections.ObjectModel;
using System.Windows.Input;
using WPF.Deepseek.Wrapper.Models;
using WPF.Deepseek.Wrapper.Services;

namespace WPF.Deepseek.Wrapper.ViewModels.Pages;

/// <summary>
/// Initializes a new instance of the <see cref="ChatViewModel"/> class.
/// </summary>
/// <param name="chatService">The chat service used to send and receive messages.</param>
public partial class ChatViewModel
    (
    IChatService chatService
    )
    : ObservableObject
{
    /// <summary>
    /// Occurs when the chat running state changes.
    /// </summary>
    public event Action<bool> ChatRunning = null!;

    /// <summary>
    /// Occurs when the caret position changes.
    /// </summary>
    public event Action<int> CaretPositionChanged = null!;

    private bool _isChatRunning = false;
    private CancellationTokenSource _cancellationTokenSource = new();

    /// <summary>
    /// Gets or sets the user input.
    /// </summary>
    [ObservableProperty]
    private string _userInput = string.Empty;

    /// <summary>
    /// Gets or sets the welcome text.
    /// </summary>
    [ObservableProperty]
    private string _welcomeText = "👋 Hello! Welcome to Deepseek AI. How can I assist you today?";

    /// <summary>
    /// Gets or sets the collection of chat messages.
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<ChatMessage> _messages = [];

    /// <summary>
    /// Gets or sets a value indicating whether the message is from a user.
    /// </summary>
    [ObservableProperty]
    private bool _isUserMessage;

    /// <summary>
    /// Gets or sets the content of the chat message.
    /// </summary>
    [ObservableProperty]
    private string _content = string.Empty;

    /// <summary>
    /// Sends the user message.
    /// </summary>
    [RelayCommand]
    private void OnSendMessage()
    {
        if (string.IsNullOrWhiteSpace(UserInput))
            return;

        WelcomeText = string.Empty;

        if (_isChatRunning)
        {
            _cancellationTokenSource.Cancel();
            _isChatRunning = false;
            UserInput = string.Empty;
            return;
        }

        _isChatRunning = true;
        _cancellationTokenSource = new CancellationTokenSource();

        Application.Current.Dispatcher.Invoke(async () =>
        {
            // Add user message to the chat
            Messages.Add(new ChatMessage { Content = UserInput, IsUserMessage = true });

            // Create a placeholder AI message for streaming response
            var aiMessage = new ChatMessage { Content = string.Empty, IsUserMessage = false };
            Messages.Add(aiMessage);

            // Call the streaming chat service
            await foreach (var chunk in chatService.SendMessageAsync(UserInput, _cancellationTokenSource.Token))
            {
                foreach (char letter in chunk) // Process each character individually
                {
                    aiMessage.Content += letter; // Append character dynamically
                    await Task.Delay(1); // Small delay for typing effect
                }
            }

            // Clear the user input
            UserInput = string.Empty;
            _isChatRunning = false;
            ChatRunning?.Invoke(_isChatRunning);
        });
    }

    /// <summary>
    /// Checks if the Enter key is pressed and sends the message.
    /// </summary>
    /// <param name="e">The key event arguments.</param>
    [RelayCommand]
    private void OnCheckEnterKey(KeyEventArgs e)
    {
        if (e.Key == Key.Enter &&
            !string.IsNullOrEmpty(UserInput))
        {
            OnSendMessage();
        }
    }

    /// <summary>
    /// Fires the caret position changed event.
    /// </summary>
    /// <param name="length">The length of the caret position.</param>
    public void FireCaretPositionChanged(int length)
        => CaretPositionChanged?.Invoke(length);
}


