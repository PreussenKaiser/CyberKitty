using CyberKitty.Models;
using CyberKitty.Modules.Modals;
using CyberKitty.Services;
using Discord;
using Discord.Interactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CyberKitty.Modules;

/// <summary>
/// The module that's responsible for event commands.
/// </summary>
public class EventModule : InteractionModuleBase<SocketInteractionContext>
{
    /// <summary>
    /// The database to club query events with.
    /// </summary>
    private readonly ClubContext database;

    /// <summary>
    /// Initializes a new instance of the EventModule module.
    /// </summary>
    public EventModule(IServiceProvider services)
        => this.database = services.GetRequiredService<ClubContext>();
    
    /// <summary>
    /// Creates a club event.
    /// </summary>
    [SlashCommand("create", "Creates an event.")]
    public async Task Create()
        => await this.RespondWithModalAsync<EventModal>("create_event");

    /// <summary>
    /// Displays all club events along with their details.
    /// </summary>
    [SlashCommand("read", "Get a list of all events.")]
    public async Task Read()
    {
        var events = await this.database.ClubEvents.ToListAsync();
        string msg = string.Empty;

        if (!events.Any())
        {
            msg = "There are no scheduled events";
        }
        else
        {
            events.ForEach(e => msg += $"{e}\n");
        }

        await this.RespondAsync(msg);
    }

    /// <summary>
    /// Allows the user to edit a specific event.
    /// </summary>
    [SlashCommand("update", "Edit the properties of an event.")]
    public async Task Update()
    {

    }

    /// <summary>
    /// Deletes the specified event.
    /// </summary>
    [SlashCommand("delete", "Remove an event.")]
    public async Task Delete()
    {
        var menu = new SelectMenuBuilder()
            .WithPlaceholder("Select the event")
            .WithCustomId("event_delete");

        if (this.BuildEventsSelect(ref menu))
        {
            var builder = new ComponentBuilder()
                .WithSelectMenu(menu);

            await this.RespondAsync(components: builder.Build());
        }
        else
        {
            await this.RespondAsync("There are no events to delete.");
        }
    }

    /// <summary>
    /// Handles the response for the create modal.
    /// </summary>
    /// <param name="modal">The modal to respond to.</param>
    [ModalInteraction("create_event")]
    public async Task CreateResponse(EventModal modal)
    {
        var clubEvent = new ClubEvent()
        {
            Name = modal.Name,
            Details = modal.Details,
            Location = modal.Location,
            Date = modal.Date
        };

        await this.database.AddAsync(clubEvent);
        await this.database.SaveChangesAsync();

        await this.RespondAsync($"Event created!\n{clubEvent}");
    }

    /// <summary>
    /// Handles the response for the delete_event selection.
    /// </summary>
    /// <param name="selectedEvent"></param>
    /// <returns></returns>
    [ComponentInteraction("event_delete")]
    public async Task DeleteResponse(string[] selectedEvent)
    {
        var events = await this.database.ClubEvents.ToListAsync();
        var clubEvent = events.Find(e => e.Id == int.Parse(selectedEvent[0]));
        var response = string.Empty;

        if (clubEvent is null)
        {
            response = "There was a problem deleting the event";
        }
        else
        {
            this.database.Remove(clubEvent);
            await this.database.SaveChangesAsync();

            response = $"{clubEvent.Name} has been deleted";
        }

        await this.RespondAsync(response);
    }

    /// <summary>
    /// Builds a select menu with current events.
    /// </summary>
    /// <param name="customId">The custom id to apply to the menu.</param>
    /// <returns>The built select menu.</returns>
    private bool BuildEventsSelect(ref SelectMenuBuilder menu)
    {
        var events = this.database.ClubEvents.ToList();

        if (!events.Any())
            return false;

        foreach (ClubEvent clubEvent in events)
            menu.AddOption
            (
                clubEvent.Name,
                clubEvent.Id.ToString(),
                clubEvent.Details
            );

        return true;
    }
}