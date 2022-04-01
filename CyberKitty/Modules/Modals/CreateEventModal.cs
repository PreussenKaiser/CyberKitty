using Discord;
using Discord.Interactions;

namespace CyberKitty.Modules.Modals;

/// <summary>
/// The modal displays inputs for creating an event.
/// </summary>
public class CreateEventModal : IModal
{
    /// <summary>
    /// The title of the modal.
    /// </summary>
    public virtual string Title => "Create Event";

    /// <summary>
    /// What the event is about.
    /// </summary>
    [InputLabel("Name")]
    [ModalTextInput("name", maxLength: 32)]
    public string Name { get; set; }

    /// <summary>
    /// The event's description.
    /// </summary>
    [InputLabel("Details")]
    [ModalTextInput("desc", TextInputStyle.Paragraph, maxLength: 512)]
    public string Details { get; set; }

    /// <summary>
    /// Where the event happens.
    /// </summary>
    [InputLabel("Location")]
    [ModalTextInput("location", placeholder: "SP Room 323", maxLength: 32)]
    public string Location { get; set; }

    /// <summary>
    /// When the event happens.
    /// </summary>
    [InputLabel("Date")]
    [ModalTextInput("date", placeholder: "2022-09-31 16:32", maxLength: 64)]
    public DateTime Date { get; set; }
}