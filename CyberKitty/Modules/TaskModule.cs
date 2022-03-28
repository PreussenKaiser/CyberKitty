using Discord.Commands;

namespace CyberKitty.Modules;

/// <summary>
/// The command module that contains actions for tasks.
/// </summary>
[Group("task")]
public class TaskModule : ModuleBase<SocketCommandContext>
{
    /// <summary>
    /// Displays the create task dialog.
    /// </summary>
    [Command("create")]
    [Summary("Shows the create task dialog.")]
    public async Task CreateTask()
    {
        await this.ReplyAsync("Hello!");
    }

    /// <summary>
    /// Displays a list of commands.
    /// </summary>
    [Command("help")]
    [Summary("Shows a list of commands.")]
    public async Task ShowHelp()
        => await this.ReplyAsync
        (
            "create: Add a new task.\n" +
            "read: Get all current tasks.\n" +
            "update: Modify an existing task.\n" +
            "delete: Remove an existing task.\n"
        );
}