using Discord;
using Discord.WebSocket;

namespace CyberKitty;

/// <summary>
/// 
/// </summary>
public class Program
{
    /// <summary>
    /// 
    /// </summary>
    private DiscordSocketClient client;
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public static Task Main(string[] args) => new Program().MainAsync();

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private async Task MainAsync()
    {
        this.client = new DiscordSocketClient();
        string token = File.ReadAllText("token.txt");
        
        this.client.Log += this.Log;

        await this.client.LoginAsync(TokenType.Bot, token);
        await this.client.StartAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    private Task Log(LogMessage message)
    {
        Console.WriteLine(message.ToString());

        return Task.CompletedTask;
    }
}
