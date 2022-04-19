using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace CyberKitty.Services;

/// <summary>
/// The class that represents a logger.
/// </summary>
public class Logger
{
    /// <summary>
    /// Attaches log events.
    /// </summary>
    /// <param name="services">The service provider to inject with.</param>
    public Logger(IServiceProvider services)
        => services
            .GetRequiredService<DiscordSocketClient>()
            .Log += this.LogAsync;

    /// <summary>
    /// Logs messages to the console.
    /// </summary>
    /// <param name="message">The log to write.</param>
    /// <returns>If the task was completed or not.</returns>
    private Task LogAsync(LogMessage message)
    {
        if (message.Exception is CommandException cmdException)
        {
            Console.WriteLine($"[Command/{message.Severity}] {cmdException.Command.Aliases.First()}"
                              + $" failed to execute in {cmdException.Context.Channel}.");
            Console.WriteLine(cmdException);
        }
        else 
            Console.WriteLine($"[General/{message.Severity}] {message}");

        return Task.CompletedTask;
    }
}