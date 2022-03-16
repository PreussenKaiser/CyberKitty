using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace CyberKitty.Commands;

/// <summary>
/// Handles commands for a CyberKitty client.
/// </summary>
public class CommandHandler
{
    /// <summary>
    /// The current instance of the handler.
    /// </summary>
    private static CommandHandler instance;
    
    /// <summary>
    /// The client to handle commands for.
    /// </summary>
    private readonly DiscordSocketClient client;

    /// <summary>
    /// Initializes a new instance of the command handler.
    /// </summary>
    /// <param name="client">The client to handle commands for.</param>
    private CommandHandler(DiscordSocketClient client)
    {
        this.client = client;

        this.client.MessageReceived += this.HandleCommandAsync;
        this.client.ButtonExecuted += this.HandleButtonPress;
    }

    /// <summary>
    /// Gets an instance of the command handler.
    /// </summary>
    /// <param name="client">The client to handle commands for.</param>
    public static CommandHandler GetInstance(DiscordSocketClient client)
    {
        return instance ??= new CommandHandler(client);
    }

    /// <summary>
    /// Parses and handles valid commands.
    /// </summary>
    /// <param name="messageParam">The command sent to the bot.</param>
    /// <returns>If the task was completed or not.</returns>
    private async Task HandleCommandAsync(SocketMessage messageParam)
    {
        if (messageParam is not SocketUserMessage message)
            return;

        int argPos = 0;

        if (!(message.HasCharPrefix('!', ref argPos) ||
            message.HasMentionPrefix(this.client.CurrentUser, ref argPos)) ||
            message.Author.IsBot)
            return;

        switch (message.Content)
        {
            case "!create":
                await this.ActOnClubTaskAsync(message, "create");
                
                break;
            
            case "!update":
                await this.ActOnClubTaskAsync(message, "update");
                
                break;
            
            case "!delete":
                await this.ActOnClubTaskAsync(message, "delete");
                
                break;
            
            case "!help":
                this.ReplyHelp(message);
                
                break;
            
            default:
                await message.Channel.SendMessageAsync(
                    "Unknown command, enter '!help' for a list of commands.");

                break;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="action"></param>
    private async Task ActOnClubTaskAsync(SocketUserMessage message, string action)
    {
        var builder = new ComponentBuilder()
            .WithButton("Meeting", $"{action}_meeting")
            .WithButton("Event", $"{action}_event");

        await message.ReplyAsync(
            $"What would you like to {action}?",
            components: builder.Build());
    }

    /// <summary>
    /// Prints a list of commands for the bot.
    /// </summary>
    private void ReplyHelp(SocketUserMessage message)
    {
        message.Channel.SendMessageAsync(
            "!create: Create a task." +
            "\n!update: Update a task." +
            "\n!delete: Delete a task.");
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="component"></param>
    /// <returns></returns>
    private async Task HandleButtonPress(SocketMessageComponent component)
    {
        await component.RespondAsync($"You pressed {component.Data.CustomId}");
    }
}