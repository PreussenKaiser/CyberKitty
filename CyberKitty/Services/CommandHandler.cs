using System.Reflection;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace CyberKitty.Services;

/// <summary>
/// Handles commands for a CyberKitty client.
/// </summary>
public class CommandHandler
{
    /// <summary>
    /// The command service to parse commands.
    /// </summary>
    private readonly CommandService commands;
    
    /// <summary>
    /// The client to handle commands for.
    /// </summary>
    private readonly DiscordSocketClient client;

    /// <summary>
    /// The services to inject into the handler.
    /// </summary>
    private readonly IServiceProvider services;

    /// <summary>
    /// Initializes a new instance of the CommandHandler class.
    /// </summary>
    /// <param name="services">The services to inject into the handler.</param>
    public CommandHandler(IServiceProvider services)
    {
        this.commands = services.GetRequiredService<CommandService>();
        this.client = services.GetRequiredService<DiscordSocketClient>();
        this.services = services;

        this.commands.CommandExecuted += this.CommandExecutedAsync;
        this.client.MessageReceived += this.MessageReceivedAsync;
        this.client.ButtonExecuted += this.ButtonExecuted;
    }
    
    /// <summary>
    /// Adds modules from the service provider.
    /// </summary>
    public async Task InitializeAsync()
        => await this.commands.AddModulesAsync
        (
            Assembly.GetEntryAssembly(),
            this.services
        );

    /// <summary>
    /// Parses received messages.
    /// </summary>
    /// <param name="messageParam">The message parameters sent to the bot.</param>
    /// <returns>If the task was completed or not.</returns>
    private async Task MessageReceivedAsync(SocketMessage messageParam)
    {
        if (messageParam is not SocketUserMessage message)
            return;
        if (message.Source != MessageSource.User)
            return;

        int argPos = 0;
        if (!(message.HasCharPrefix('!', ref argPos) ||
              message.HasMentionPrefix(this.client.CurrentUser, ref argPos)) ||
              message.Author.IsBot)
        {
            return;
        }

        var context = new SocketCommandContext(this.client, message);
        var result = await this.commands.ExecuteAsync
        (
            context, argPos,
            this.services
        );
        
        if (result.Error is CommandError.UnknownCommand)
            await context.Channel.SendMessageAsync
            (
                "Unknown command, enter !help for a list of commands"
            );
    }

    /// <summary>
    /// Handles the result of a command execution.
    /// </summary>
    /// <param name="command">The command that was executed.</param>
    /// <param name="context">The command's context.</param>
    /// <param name="result">The result of the command.</param>
    private async Task CommandExecutedAsync
    (
        Optional<CommandInfo> command,
        ICommandContext context,
        IResult result
    )
    {
        if (!command.IsSpecified)
            return;

        if (result.IsSuccess)
            return;

        await context.Channel.SendMessageAsync($"Error: {result}");
    }

    /// <summary>
    /// Prints a list of commands for the bot.
    /// </summary>
    private void ReplyHelp(SocketUserMessage message)
    {
        message.Channel.SendMessageAsync
        (
            "!create: Create a task." +
            "\n!update: Update a task." +
            "\n!delete: Delete a task."
        );
    }
    
    /// <summary>
    /// Parses button presses.
    /// </summary>
    /// <param name="component">The button that was pressed.</param>
    /// <returns>If the task was completed or not.</returns>
    private async Task ButtonExecuted(SocketMessageComponent component)
    {
        await component.RespondAsync
        (
            $"You pressed {component.Data.CustomId}"
        );
    }
}