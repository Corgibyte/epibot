using Microsoft.EntityFrameworkCore;

namespace EpiBot.Models
{
  public class EpiBotContext : DbContext
  {
    public DbSet<GitHubTag> GitHubTags { get; set; }

    public EpiBotContext(DbContextOptions options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<GitHubTag>()
        .HasData(
          new GitHubTag { GitHubTagId = 1, Name = "Hannah Young", Email = "hannah@corgibyte.com" },
          new GitHubTag { GitHubTagId = 2, Name = "Aaron Minnick", Email = "abminnick@gmail.com" }
        );
    }
  }
}