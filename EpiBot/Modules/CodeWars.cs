using Discord;
using Discord.Commands;
using Discord.Interactions;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using System.Net.Http;
using Newtonsoft.Json.Linq;


namespace EpiBot.Modules
{
  public class CodeWarsModule : InteractionModuleBase<SocketInteractionContext>
  {
    [SlashCommand("codewars", "Returns specified user information from CodeWars")]
    public async Task CodeWars(string userName = null)
    {
      var client = new HttpClient();
      var result = await client.GetStringAsync($"https://www.codewars.com/api/v1/users/{userName}");
      JObject post = JObject.Parse(result.ToString());
      Console.WriteLine(post);
      
      var builder = new EmbedBuilder()
        .WithTitle("Username")
        .WithDescription(post["username"].ToString())
        .AddField("Honor", post["honor"].ToString(), true)
        .AddField("Overall Rank", post["ranks"]["overall"]["name"].ToString(), true)    
        .AddField("Total Kata Completed", post["codeChallenges"]["totalCompleted"].ToString(), true)
        .WithColor(new Color (0, 184, 177));
        
      var embed = builder.Build();
      await RespondAsync(embed : embed);
    }
  }
}