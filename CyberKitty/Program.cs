using CyberKitty.Services;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CyberKitty;

/// <summary>
/// The main entry point for the bot.
/// </summary>
internal class Program
{
    /// <summary>
    /// The configuration for the bot.
    /// </summary>
    private readonly IConfiguration config;

    /// <summary>
    /// Initializes a new instance of the Program entrypoint.
    /// </summary>
    private Program()
        => this.config = this.BuildConfig();
    
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

        await services.GetRequiredService<InteractionHandler>()
            .InitializeAsync();

        await Task.Delay(Timeout.Infinite);
    }

    /// <summary>
    /// Logs client processes to the console.
    /// </summary>
    /// <param name="log">The message to log.</param>
    private Task LogAsync(LogMessage log)
    {
        Console.WriteLine(log.ToString());
        
        return Task.CompletedTask;
    }

    /// <summary>
    /// Loads dependency injection services.
    /// </summary>
    /// <returns>Services for injection.</returns>
    private ServiceProvider ConfigureServices()
    {
        return new ServiceCollection()
            .AddDbContext<ClubContext>()
            .AddSingleton<DiscordSocketClient>()
            .AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()))
            .AddSingleton<InteractionHandler>()
            .BuildServiceProvider();
    }

    /// <summary>
    /// Initializes application configuration.
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
