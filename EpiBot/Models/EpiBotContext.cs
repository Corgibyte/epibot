using Microsoft.EntityFrameworkCore;

namespace EpiBot.Models
{
  public class EpiBotContext : DbContext
  {
    public DbSet<Byline> Bylines { get; set; }

    public EpiBotContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);
      builder.Entity<Byline>()
        .HasData(
          new Byline { BylineId = 1, Name = "Hannah Young", Email = "hannah@corgibyte.com" },
          new Byline { BylineId = 2, Name = "Aaron Minnick", Email = "abminnick@gmail.com" }
        );
    }
  }
}