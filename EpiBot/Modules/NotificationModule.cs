using Discord.Interactions;
using Discord.WebSocket;
using EpiBot.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace EpiBot.Models
{
  public class NotificationModule : InteractionModuleBase<SocketInteractionContext>
  {
    private readonly EpiBotContext _db;

    public NotificationModule(IServiceProvider provider)
    {
      _db = provider.GetRequiredService<EpiBotContext>();
    }

    [SlashCommand("pet-notifications", "toggle for pet notifications")]
    public async Task PetNotifications()
    {
      ulong userId = Context.User.Id;
      if(!_db.PetNotificationClients.Any(client => client.UserId == userId))
      {
        await _db.PetNotificationClients.AddAsync(new PetNotificationClient { UserId = userId });
        await _db.SaveChangesAsync();
        await RespondAsync("You have been signed up for pet notifications");
      
      }
      else 
      {
        PetNotificationClient client = await _db.PetNotificationClients.FirstOrDefaultAsync(client => client.UserId == userId);
        _db.PetNotificationClients.Remove(client);
        await _db.SaveChangesAsync();
        await RespondAsync("You will no longer receive pet notifications");
      }
    }
  }
}