using System.ComponentModel.DataAnnotations;

namespace CyberKitty.Models;

/// <summary>
/// The class that represents a club event.
/// </summary>
public class ClubEvent
{
    /// <summary>
    /// The unique identifier for the event.
    /// </summary>
    [Key]
    public int Id { get; init; }
    
    /// <summary>
    /// The name of the event.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The description of the event.
    /// </summary>
    public string Details { get; set; }
    
    /// <summary>
    /// Where the event takes place.
    /// </summary>
    public string Location { get; set; }
    
    /// <summary>
    /// When the event occurs.
    /// </summary>
    public DateTime Date { get; set; }
}