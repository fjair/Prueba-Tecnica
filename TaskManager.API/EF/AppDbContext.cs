using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Security;
using TaskManager.API.Entities;

namespace TaskManager.API.EF;

public class AppDbContext: DbContext
{
    private readonly IConfiguration _configuration;

    public AppDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("dbTaskManager"));
    }

    public DbSet<Goal> Goals { get; set; }
    public DbSet<Taask>  Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Goal>().ToTable("tblGoals");
        modelBuilder.Entity<Taask>().ToTable("tblTasks");       
    }
}
