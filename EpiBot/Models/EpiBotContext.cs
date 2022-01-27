using Microsoft.EntityFrameworkCore;

namespace EpiBot.Models
{
  public class EpiBotContext : DbContext
  {
    public DbSet<ImportantLink> ImportantLinks { get; set; }
    public virtual DbSet<Byline> Bylines { get; set; }
    public virtual DbSet<LoginReminderClient> LoginReminderClients { get; set; }
    
    public virtual DbSet<PetNotificationClient> PetNotificationClients { get; set; }
    public EpiBotContext(DbContextOptions options) : base(options) { }
  }
}
