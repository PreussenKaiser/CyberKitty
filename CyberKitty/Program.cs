using CyberKitty.Services;
using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CyberKitty;

/// <summary>
/// The main entry point for the bot.
/// </summary>
internal class Program
{
    /// <summary>
    /// 
    /// </summary>
    private IConfiguration config;

    /// <summary>
    /// Initializes a new instance of the Program entrypoint.
    /// </summary>
    private Program()
    {
        this.config = this.BuildConfig();
    }
    
    /// <summary>
    /// Translates the main entrypoint to the asynchronous entrypoint.
    /// </summary>
    /// <param name="args">Console arguments.</param>
    /// <returns>If the task was completed.</returns>
    private static void Main(string[] args)
        => new Program().MainAsync().GetAwaiter().GetResult();

    /// <summary>
    /// The main entrypoint for the bot.
    /// </summary>
    /// <returns>If the task was completed.</returns>
    private async Task MainAsync()
    {
        await using var services = this.ConfigureServices();

        var client = services.GetRequiredService<DiscordSocketClient>();
        Logger logger = new(client);
        
        await client.LoginAsync(TokenType.Bot, this.config["token"]);
        await client.StartAsync();

        //await services.GetRequiredService<CommandHandler>().InitializeAsync();
        await services.GetRequiredService<InteractionHandler>().InitializeAsync();

        await Task.Delay(Timeout.Infinite);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="log"></param>
    /// <returns></returns>
    private Task LogAsync(LogMessage log)
    {
        Console.WriteLine(log.ToString());
        
        return Task.CompletedTask;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private ServiceProvider ConfigureServices()
    {
        return new ServiceCollection()
            .AddSingleton<DiscordSocketClient>()
            .AddSingleton<CommandService>()
            .AddSingleton<CommandHandler>()
            .AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()))
            .AddSingleton<InteractionHandler>()
            .AddSingleton<Logger>()
            .AddSingleton<TaskService>()
            .BuildServiceProvider();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private IConfiguration BuildConfig()
    {
        return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(@"config.json")
            .Build();
    }
}
