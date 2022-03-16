namespace CyberKitty.Models;

/// <summary>
/// The class that represents an event.
/// </summary>
public class ClubEvent : ClubTask
{
    /// <summary>
    /// The unique identifier for the event.
    /// </summary>
    public override int Id { get; init; }
    
    /// <summary>
    /// The title of the event.
    /// </summary>
    public override string Title { get; set; }
    
    /// <summary>
    /// The event description.
    /// </summary>
    public override string Description { get; set; }
    
    /// <summary>
    /// Where the event takes place.
    /// </summary>
    public override string Location { get; set; }
    
    /// <summary>
    /// When the event happens.
    /// </summary>
    public override DateTime Date { get; set; }
}