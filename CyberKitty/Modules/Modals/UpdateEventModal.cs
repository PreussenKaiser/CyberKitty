using Discord.Interactions;

namespace CyberKitty.Modules.Modals;

/// <summary>
/// The modal that displays inputs for updating an event.
/// </summary>
public class UpdateEventModal : CreateEventModal, IModal
{
    /// <summary>
    /// The title for the modal.
    /// </summary>
    public override string Title => "Update Event";
    
    /// <summary>
    /// The ID of the event to update.
    /// </summary>
    [InputLabel("ID")]
    [ModalTextInput("id")]
    public string Id { get; set; }
}