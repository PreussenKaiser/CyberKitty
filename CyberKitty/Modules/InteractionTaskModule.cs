using Discord;
using Discord.Interactions;

namespace CyberKitty.Modules;

/// <summary>
/// 
/// </summary>
[Group("interaction", "Responsible for task.")]
public class InteractionTaskModule : InteractionModuleBase<SocketInteractionContext>
{
    [MessageCommand("test")]
    public async Task Test(IMessage message)
    {
        await this.ReplyAsync("hello!");
    }
}