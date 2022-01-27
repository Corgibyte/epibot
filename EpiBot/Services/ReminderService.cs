using Discord;
using Discord.WebSocket;
using EpiBot.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EpiBot.Services
{
  public class ReminderService
  {
    private System.Threading.Timer _timer = null!;
    private readonly DiscordSocketClient _discord;
    private readonly EpiBotContext _db;

    public ReminderService(DiscordSocketClient discord, IServiceProvider services)
    {
      _discord = discord;
      _db = services.GetRequiredService<EpiBotContext>();
      #pragma warning disable 4014
      StartAsync();
    }

    private async Task StartAsync()
    {
      _timer = new Timer(CheckReminders, null, TimeSpan.Zero, TimeSpan.FromSeconds(60));
      await Task.CompletedTask;
    }

    private void CheckReminders(object state)
    {
      CheckLoginReminder();
      CheckLogoutReminder();
    }

    private async void CheckLoginReminder()
    {
      bool correctDay = !(DateTime.Now.DayOfWeek == DayOfWeek.Friday || DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday);
      if (correctDay)
      {
        bool amTest = DateTime.Now.Hour == 7;
        amTest = amTest && DateTime.Now.Minute >= 45 && DateTime.Now.Minute < 49;
        if (amTest)
        {
          foreach (LoginReminderClient client in _db.LoginReminderClients)
          {
            if (DateTime.Now.Date > client.LastAMReminder.Date)
            {
              IUser user = await _discord.GetUserAsync(client.UserId);
              IDMChannel channel = await user.CreateDMChannelAsync();
              await channel.SendMessageAsync("Epicodus reminder: don't forget to login!");
              client.LastAMReminder = DateTime.Now;
              _db.Entry(client).State = EntityState.Modified;
              await _db.SaveChangesAsync();
            }
          }
        }
      }
    }

    private async void CheckLogoutReminder()
    {
      bool correctDay = !(DateTime.Now.DayOfWeek == DayOfWeek.Friday || DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday);
      if (correctDay)
      {
        bool pmTest = DateTime.Now.Hour == 16;
        pmTest = pmTest && DateTime.Now.Minute >= 45 && DateTime.Now.Minute < 59;
        if (pmTest)
        {
          foreach (LoginReminderClient client in _db.LoginReminderClients)
          {
            if (DateTime.Now.Date > client.LastPMReminder.Date)
            {
              IUser user = await _discord.GetUserAsync(client.UserId);
              IDMChannel channel = await user.CreateDMChannelAsync();
              await channel.SendMessageAsync("Epicodus reminder: don't forget to logout!");
              client.LastPMReminder = DateTime.Now;
              _db.Entry(client).State = EntityState.Modified;
              await _db.SaveChangesAsync();
            }
          }
        }
      }
    }
  }
}
