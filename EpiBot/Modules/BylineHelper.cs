using Discord.Interactions;
using EpiBot.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

    [SlashCommand("byline-generate", "create byline with provided names")]
    public async Task BylineGenerate(string name1, string name2 = "", string name3 = "", string name4 = "")
    {
      List<Byline> bylines = new List<Byline>();
      bylines.Add(await _db.Bylines.FirstOrDefaultAsync(byline => byline.Name == name1));
      if (name2 != "")
      {
        bylines.Add(await _db.Bylines.FirstOrDefaultAsync(byline => byline.Name == name2));
      }
      if (name3 != "")
      {
        bylines.Add(await _db.Bylines.FirstOrDefaultAsync(byline => byline.Name == name3));
      }
      if (name4 != "")
      {
        bylines.Add(await _db.Bylines.FirstOrDefaultAsync(byline => byline.Name == name4));
      }
      string response = "";
      foreach (Byline byline in bylines)
      {
        response += byline.ToString();
        response += "\n";
      }
      await RespondAsync(response);
    }
  }
}