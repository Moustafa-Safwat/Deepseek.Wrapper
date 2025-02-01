namespace WPF.Deepseek.Wrapper.Services;

/// <summary>
/// Defines the contract for a chat service that can send messages asynchronously.
/// </summary>
public interface IChatService
{
    /// <summary>
    /// Sends a message asynchronously and returns a stream of responses.
    /// </summary>
    /// <param name="userMessage">The message to be sent by the user.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>An asynchronous stream of response messages.</returns>
    IAsyncEnumerable<string> SendMessageAsync(string userMessage, CancellationToken cancellationToken);
}
