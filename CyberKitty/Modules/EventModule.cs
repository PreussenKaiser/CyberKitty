using CyberKitty.Models;
using CyberKitty.Modules.Modals;
using CyberKitty.Services;
using Discord.Interactions;
using Microsoft.EntityFrameworkCore;

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
    public EventModule()
    {
        this.database = new ClubContext();
    }
    
    /// <summary>
    /// Creates a club event.
    /// </summary>
    [SlashCommand("create", "Creates an event.")]
    public async Task Create()
        => await this.RespondWithModalAsync<CreateEventModal>("create_event");

    /// <summary>
    /// Displays all club events along with their details.
    /// </summary>
    [SlashCommand("read", "Get a list of all events.")]
    public async Task Read()
    {
        string msg = string.Empty;

        await this.database.ClubEvents.ForEachAsync(e => 
            msg += e + "\n"
        );

        await this.RespondAsync(msg);
    }

    /// <summary>
    /// Allows the user to edit a specific event.
    /// </summary>
    [SlashCommand("update", "Edit the properties of an event.")]
    public async Task Update()
        => await this.RespondWithModalAsync<UpdateEventModal>("update_event");

    /// <summary>
    /// Deletes the specified event.
    /// </summary>
    [SlashCommand("delete", "Remove an event.")]
    public async Task Delete()
        => await this.RespondWithModalAsync<DeleteEventModal>("delete_event");

    /// <summary>
    /// Handles the response for the create modal.
    /// </summary>
    /// <param name="modal">The modal to respond to.</param>
    [ModalInteraction("create_event")]
    public async Task CreateResponse(CreateEventModal modal)
    {
        this.database.Add(new ClubEvent()
        {
            Name = modal.Name,
            Details = modal.Details,
            Location = modal.Location,
            Date = modal.Date
        });

        await this.RespondAsync
        (
            "Event created!\n" +
            $"**What:** {modal.Name}\n" +
            $"**When:** {modal.Location}\n" +
            $"**Where:** {modal.Date}\n" +
            $"**Details:** {modal.Details}"
        );
    }

    /// <summary>
    /// Handles the response for the update modal.
    /// </summary>
    /// <param name="model">The modal to respond to.</param>
    [ModalInteraction("update_event")]
    public async Task UpdateResponse(UpdateEventModal model)
    {
    }

    /// <summary>
    /// Handles the response for the delete modal.
    /// </summary>
    /// <param name="model">The modal to respond to.</param>
    [ModalInteraction("delete_event")]
    public async Task DeleteResponse(DeleteEventModal model)
    {
    }
    
}