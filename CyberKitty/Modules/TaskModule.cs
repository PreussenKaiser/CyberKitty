using Discord;
using Discord.Commands;

namespace CyberKitty.Modules;

/// <summary>
/// The command module that contains actions for tasks.
/// </summary>
public class TaskModule : ModuleBase<SocketCommandContext>
{
    /// <summary>
    /// Displays the create task dialog.
    /// </summary>
    [Command("create")]
    [Summary("Shows create task dialog.")]
    public async Task CreateTask()
    {
        await this.BuildDialog("create");
    }

    /// <summary>
    /// Displays the update task dialog.
    /// </summary>
    [Command("update")]
    [Summary("Show update task dialog.")]
    public async Task UpdateTask()
    {
        await this.BuildDialog("update");
    }

    /// <summary>
    /// Displays that delete task dialog.
    /// </summary>
    [Command("delete")]
    [Summary("Shows delete task dialog.")]
    public async Task DeleteTask()
    {
        await this.BuildDialog("delete");
    }
    
    /// <summary>
    /// Builds a dialog for tasks.
    /// </summary>
    /// <param name="action">The action to take for a task.</param>
    private async Task BuildDialog(string action)
    {
        var builder = new ComponentBuilder()
            .WithButton("Meeting", $"{action}_meeting")
            .WithButton("Event", $"{action}_event");
        
        await this.ReplyAsync
        (
            $"What would you like to {action}?",
            components: builder.Build()
        );
    }
}