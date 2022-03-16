namespace CyberKitty.Models;

/// <summary>
/// The class that represents a meeting.
/// </summary>
public class ClubMeeting : ClubTask
{
    /// <summary>
    /// The unique identifier for the meeting.
    /// </summary>
    public override int Id { get; init; }
    
    /// <summary>
    /// The title of the meeting.
    /// </summary>
    public override string Title { get; set; }
    
    /// <summary>
    /// What the meetings is about.
    /// </summary>
    public override string Description { get; set; }
    
    /// <summary>
    /// Where the meeting will happen.
    /// </summary>
    public override string Location { get; set; }
    
    /// <summary>
    /// When the meeting happens.
    /// </summary>
    public override DateTime Date { get; set; }
}