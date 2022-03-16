using Discord;
using Discord.WebSocket;

namespace CyberKitty;

/// <summary>
/// Handles logging for the CyberKitty bot.
/// </summary>
public class Logger
{
    /// <summary>
    /// The current instance of the logger.
    /// </summary>
    private static Logger instance;

    /// <summary>
    /// Initializes the logger and sets the provided client to log to it.
    /// </summary>
    /// <param name="client">The client to attack to.</param>
    private Logger(DiscordSocketClient client)
    {
        client.Log += this.Log;
    }

    /// <summary>
    /// Gets the current instance of the logger singleton.
    /// </summary>
    /// <returns>The logger singleton.</returns>
    public static Logger GetInstance(DiscordSocketClient client)
    {
        return instance ??= new Logger(client);
    }

    /// <summary>
    /// Logs a message to the console.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <returns>If the message was logged or not.</returns>
    private Task Log(LogMessage message)
    {
        Console.WriteLine(message.ToString());
        
        return Task.CompletedTask;
    }
}