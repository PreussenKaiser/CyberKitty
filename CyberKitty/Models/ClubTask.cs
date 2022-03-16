namespace CyberKitty.Models;

/// <summary>
/// The class that represents a club task.
/// </summary>
public abstract class ClubTask
{
    /// <summary>
    /// The unique identifier for the task.
    /// </summary>
    public abstract int Id { get; init; }
    
    /// <summary>
    /// The title of the task.
    /// </summary>
    public abstract string Title { get; set; }

    /// <summary>
    /// The description of the task.
    /// </summary>
    public abstract string Description { get; set; }
    
    /// <summary>
    /// Where the task takes place.
    /// </summary>
    public abstract string Location { get; set; }
    
    /// <summary>
    /// When the task happens.
    /// </summary>
    public abstract DateTime Date { get; set; }
}