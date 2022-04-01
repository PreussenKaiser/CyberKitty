using CyberKitty.Modules.Modals;
using CyberKitty.Services;
using Discord.Interactions;

namespace CyberKitty.Modules;

/// <summary>
/// The module that's responsible for event commands.
/// </summary>
[Group("event", "Commands for events.")]
public class EventModule : InteractionModuleBase<SocketInteractionContext>
{
    /// <summary>
    /// The database context to use.
    /// </summary>
    private readonly ClubContext db;

    /// <summary>
    /// Initializes a new instance of the EventModule module.
    /// </summary>
    /// <param name="db">The database context to use.</param>
    public EventModule(ClubContext db)
        => this.db = db;

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
        // reply with all events
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
        await this.RespondAsync
        (
            "Event created!\n" +
            $"What: {modal.Name}\n" +
            $"When: {modal.Location}\n" +
            $"Where: {modal.Date}\n\n" +
            $"Details: {modal.Details}"
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