using System.Reflection;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace CyberKitty.Services;

/// <summary>
/// 
/// </summary>
public class InteractionHandler
{
    /// <summary>
    /// 
    /// </summary>
    private readonly InteractionService interactions;
    
    /// <summary>
    /// 
    /// </summary>
    private readonly DiscordSocketClient client;
    
    /// <summary>
    /// 
    /// </summary>
    private readonly IServiceProvider services;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    public InteractionHandler(IServiceProvider services)
    {
        this.interactions = services.GetRequiredService<InteractionService>();
        this.client = services.GetRequiredService<DiscordSocketClient>();
        this.services = services;
    }

    /// <summary>
    /// 
    /// </summary>
    public async Task InitializeAsync()
    {
        this.client.Ready += this.ReadyAsync;

        await this.interactions.AddModulesAsync
        (
            Assembly.GetEntryAssembly(),
            this.services
        );

        this.client.InteractionCreated += this.HandleInteraction;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private async Task ReadyAsync()
    {
        await this.interactions
            .RegisterCommandsGloballyAsync(true);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="interaction"></param>
    private async Task HandleInteraction(SocketInteraction interaction)
    {
        try
        {
            await this.interactions.ExecuteCommandAsync
            (
                new SocketInteractionContext(this.client, interaction),
                this.services
            );
        }
        catch
        {
            if (interaction.Type is InteractionType.ApplicationCommand)
                await interaction
                    .GetOriginalResponseAsync()
                    .ContinueWith(async (msg) 
                        => await msg.Result.DeleteAsync());
        }
    }
}