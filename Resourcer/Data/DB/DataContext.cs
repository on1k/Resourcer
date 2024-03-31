using Microsoft.EntityFrameworkCore;
using Resourcer.Data.DB.Models;

namespace Resourcer.Data.DB;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Resource>().HasKey(t => t.Id);
    }

    public DbSet<Resource> Resources { get; set; }
}