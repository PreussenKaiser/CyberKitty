using CyberKitty.Models;
using Microsoft.EntityFrameworkCore;

namespace CyberKitty.Services;

/// <summary>
/// The database context to query club activities.
/// </summary>
public class ClubContext : DbContext
{
    /// <summary>
    /// Club tasks from the database.
    /// </summary>
    public DbSet<ClubEvent> ClubEvents { get; set; }

    /// <summary>
    /// Contains the connection string to the SQL server database.
    /// </summary>
    private readonly string connectionString;

    /// <summary>
    /// Initializes the connection.
    /// </summary>
    public ClubContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);

        this.connectionString = Path.Join(path, "club.db");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={this.connectionString}");
}