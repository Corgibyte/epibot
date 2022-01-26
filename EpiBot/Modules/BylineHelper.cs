using Discord.Interactions;
using System.Threading.Tasks;
using EpiBot.Models;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace EpiBot.Modules
{
  public class BylineHelper : InteractionModuleBase<SocketInteractionContext>
  {
    private readonly EpiBotContext _db;

    public BylineHelper(IServiceProvider services)
    {
      _db = services.GetRequiredService<EpiBotContext>();
    }

    [SlashCommand("byline-register", "register new byline for user")]
    public async Task BylineRegister(string name, string email)
    {
      _db.Bylines.Add(new Byline { Name = name, Email = email });
      var result = _db.SaveChanges();
      if (result == 1)
      {
        await RespondAsync("Byline added");
      }
      else
      {
        await RespondAsync("Unable to add byline");
      }
    }
  }
}