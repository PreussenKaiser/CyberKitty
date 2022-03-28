using System.Reflection;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using IResult = Discord.Commands.IResult;

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
        var result = await this.interactions.ExecuteCommandAsync
        (
            new SocketInteractionContext(this.client, interaction),
            this.services
        );
    }
}