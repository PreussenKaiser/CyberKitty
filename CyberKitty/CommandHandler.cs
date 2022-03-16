using Discord.Commands;
using Discord.WebSocket;

namespace CyberKitty;

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
    /// 
    /// </summary>
    /// <param name="messageParam"></param>
    /// <returns></returns>
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
                this.CreateTask(message);
                
                break;
            
            case "!update":
                this.UpdateTask(message);
                
                break;
            
            case "!delete":
                this.DeleteTask(message);
                
                break;
        }
    }

    /// <summary>
    /// Handles creation commands.
    /// </summary>
    /// <param name="message">The entered create command.</param>
    private void CreateTask(SocketUserMessage message)
    {
        message.Channel.SendMessageAsync("What would you like to create?");
    }
    
    /// <summary>
    /// Handles update commands.
    /// </summary>
    /// <param name="message">The entered update command.</param>
    private void UpdateTask(SocketUserMessage message)
    {
        message.Channel.SendMessageAsync("What would you like to update?");
    }

    /// <summary>
    /// Handles delete commands.
    /// </summary>
    /// <param name="message">The entered delete command.</param>
    private void DeleteTask(SocketUserMessage message)
    {
        message.Channel.SendMessageAsync("What would you like to delete?");
    }
}