using Discord.Interactions;
using EpiBot.Models;
using Microsoft.EntityFrameworkCore;
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
      string response = "";
      int order = 1;

      if (links.Count == 0)
      {
        await RespondAsync("Important links not found!!");
      }
      else
      {
        foreach( ImportantLink link in links)
        {
          response += order++ + ". " + link.Description.ToString() + "{" + link.ImportantLinkId + "}" + " - " + link.Link.ToString();
          response += "\n";
        }        
      }
      await RespondAsync(response); 
    }
  
    [SlashCommand("importantlink-delete", "Delete link")]
    public async Task ImportantLinkDelete(int Id)
    {
      ImportantLink thisLink = await _db.ImportantLinks.FirstOrDefaultAsync(find => find.ImportantLinkId == Id);

      if (thisLink == null)
      {
        await RespondAsync("Link does not exist, check the link 'Id'");
      }

      _db.ImportantLinks.Remove(thisLink);
      _db.SaveChanges();

      await RespondAsync($"Important link '{thisLink.Description}' has been deleted!");
    }
  }
}  