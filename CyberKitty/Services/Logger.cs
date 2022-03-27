using Discord;
using Discord.WebSocket;

namespace CyberKitty.Services;

/// <summary>
/// The class that represents a logger.
/// </summary>
public class Logger
{
    /// <summary>
    /// Attaches log events.
    /// </summary>
    /// <param name="client">The client to attack logging to.</param>
    public Logger(DiscordSocketClient client)
        => client.Log += this.LogAsync;

    /// <summary>
    /// Logs messages to the console.
    /// </summary>
    /// <param name="log">The log to write.</param>
    /// <returns>If the task was completed or not.</returns>
    private Task LogAsync(LogMessage log)
    {
        Console.WriteLine(log.ToString());
        return Task.CompletedTask;
    }
}