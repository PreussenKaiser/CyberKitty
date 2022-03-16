using Discord;
using Discord.WebSocket;

namespace CyberKitty;

/// <summary>
/// The main entry point for the bot.
/// </summary>
public class Program
{
    /// <summary>
    /// The bots client.
    /// </summary>
    private readonly DiscordSocketClient client = new();

    /// <summary>
    /// Handles commands for the client.
    /// </summary>
    private CommandHandler commandHandler;

    /// <summary>
    /// Logs events to the console.
    /// </summary>
    private Logger logger;

    /// <summary>
    /// Translates the main entrypoint to the asynchronous entrypoint.
    /// </summary>
    /// <param name="args">Console arguments.</param>
    /// <returns>If the task was completed.</returns>
    public static Task Main(string[] args) => new Program().MainAsync();

    /// <summary>
    /// The main entrypoint for the bot.
    /// </summary>
    /// <returns>If the task was completed.</returns>
    private async Task MainAsync()
    {
        string token = await File.ReadAllTextAsync(@"token.txt");
        this.commandHandler = CommandHandler.GetInstance(this.client);
        this.logger = Logger.GetInstance(this.client);
        
        await this.client.LoginAsync(TokenType.Bot, token);
        await this.client.StartAsync();

        await Task.Delay(-1);
    }
}
