using Microsoft.EntityFrameworkCore;

namespace EpiBot.Models
{
  public class EpiBotContext : DbContext
  {
    public virtual DbSet<Byline> Bylines { get; set; }
    public virtual DbSet<LoginReminderClient> LoginReminderClients { get; set; }

    public EpiBotContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);      
    }
  }
}
