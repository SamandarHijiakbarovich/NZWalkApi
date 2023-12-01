using Microsoft.EntityFrameworkCore;
using NzWalks.Api.Models.Domain;

namespace NzWalks.Api.Data;

public class AppDbContext:DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
        
    }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Walk> Walks { get; set; }
    public DbSet<Difficult> Difficults { get; set; }

}
