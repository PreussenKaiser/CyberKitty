using System.ComponentModel.DataAnnotations;

namespace CyberKitty.Models;

/// <summary>
/// The class that represents a club task.
/// </summary>
public class ClubTask
{
    /// <summary>
    /// The unique identifier for the task.
    /// </summary>
    [Key]
    public int Id { get; init; }
    
    /// <summary>
    /// The title of the task.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// The description of the task.
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// Where the task takes place.
    /// </summary>
    public string Location { get; set; }
    
    /// <summary>
    /// When the task happens.
    /// </summary>
    public DateTime Date { get; set; }
}