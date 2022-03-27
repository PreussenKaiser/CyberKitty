using CyberKitty.Models;
using Microsoft.EntityFrameworkCore;

namespace CyberKitty.Services;

/// <summary>
/// The database context to query club activities.
/// </summary>
public class TaskService : DbContext
{
    /// <summary>
    /// Club tasks from the database.
    /// </summary>
    public DbSet<ClubTask> ClubTasks { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="optionsBuilder"></param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=ClubDB.db");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClubTask>().ToTable("Tasks");
    }
}