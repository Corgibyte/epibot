using Discord.Interactions;
using EpiBot.Models;
using FuzzySharp;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EpiBot.Modules
{

  public class ImportantLinkModule : InteractionModuleBase<SocketInteractionContext>
  {
    private readonly EpiBotContext _db;

    public ImportantLinkModule(IServiceProvider services)
    {
      _db = services.GetRequiredService<EpiBotContext>();
    }

    [SlashCommand("importantlink-add", "add important link")]
    public async Task ImportantLinkAdd(string description, string link)
    {
      _db.ImportantLinks.Add(new ImportantLink { Description = description, Link = link});
      var result = _db.SaveChanges();
      if (result == 1)
      {
        await RespondAsync("Link added");
      }
      else
      {
        await RespondAsync("Unable to add link");
      }
    }

    [SlashCommand("importantlink-view", "view list of important links")]
    public async Task ImportantLinkView()
    {
      List<ImportantLink> links = new List<ImportantLink>(_db.ImportantLinks);
      Console.WriteLine(links);
      string response = "";
      bool foundAll = true;
      foreach (ImportantLink link in links)
      {
        if (link != null)
        {
          response += link.Description.ToString() + " - " + link.Link.ToString();
          // response += " - ";
          // response += link.Link.ToString();
          response += "\n";
        }
        else
        {
          foundAll = false;
        } 
      }
      if (foundAll)
      {
        await RespondAsync(response);
      }
      else
      {
        await RespondAsync("Important links are not found!!");
      }
    }
  }
}