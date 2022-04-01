using Discord.Interactions;

namespace CyberKitty.Modules.Modals;

/// <summary>
/// The modal that displays inputs for deleting an event.
/// </summary>
public class DeleteEventModal : IModal
{
    /// <summary>
    /// The title of the modal.
    /// </summary>
    public string Title => "Delete Event";
    
    /// <summary>
    /// The id of the event to delete.
    /// </summary>
    [InputLabel("ID")]
    [ModalTextInput("id")]
    public string Id { get; set; }
}