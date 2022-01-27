using Discord.Interactions;
using Discord.WebSocket;
using EpiBot.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EpiBot.Modules
{
  public class ReminderModule : InteractionModuleBase<SocketInteractionContext>
  {
    private readonly IConfigurationRoot _config;
    private readonly IServiceProvider _provider;
    private readonly EpiBotContext _db;

    public ReminderModule(IConfigurationRoot config, IServiceProvider provider)
    {
      _config = config;
      _provider = provider;
      _db = provider.GetRequiredService<EpiBotContext>();
    }

    [SlashCommand("login-reminders", "toggle for login reminders")]
    public async Task LoginReminders()
    {
      ulong userId = Context.User.Id;
      if (!_db.LoginReminderClients.Any(client => client.UserId == userId))
      {
        await _db.LoginReminderClients.AddAsync(new LoginReminderClient { UserId = userId });
        await _db.SaveChangesAsync();
        await RespondAsync("You have been signed up for login/logout reminders! Use the command again to remove registration.", ephemeral: true);
      }
      else
      {
        LoginReminderClient client = await _db.LoginReminderClients.FirstOrDefaultAsync(client => client.UserId == userId);
        _db.LoginReminderClients.Remove(client);
        await _db.SaveChangesAsync();
        await RespondAsync("You will no longer receive login/logout reminders!", ephemeral: true);
      }
    }


  }
}