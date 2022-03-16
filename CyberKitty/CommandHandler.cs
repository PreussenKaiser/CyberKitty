using System.Net.WebSockets;
using Discord.Commands;
using Discord.WebSocket;

namespace CyberKitty;

/// <summary>
/// 
/// </summary>
public class CommandHandler
{
    /// <summary>
    /// 
    /// </summary>
    private readonly DiscordSocketClient client;

    /// <summary>
    /// 
    /// </summary>
    private readonly CommandService commands;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="client"></param>
    /// <param name="commands"></param>
    public CommandHandler(DiscordSocketClient client, CommandService commands)
    {
        this.client = client;
        this.commands = commands;
    }

    /// <summary>
    /// 
    /// </summary>
    public async Task InstallCommandsAsync()
    {
        this.client.MessageReceived += this.HandleCommandAsync;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="messageParam"></param>
    /// <returns></returns>
    private async Task HandleCommandAsync(SocketMessage messageParam)
    {
        var message = messageParam as SocketUserMessage;
        if (message == null)
            return;

        int argPos = 0;

        if (!(message.HasCharPrefix('!', ref argPos) ||
            message.HasMentionPrefix(this.client.CurrentUser, ref argPos)) ||
            message.Author.IsBot)
            return;

        SocketCommandContext context = new(this.client, message);
    }
}