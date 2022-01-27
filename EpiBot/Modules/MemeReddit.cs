using Discord;
using Discord.Commands;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using System.Net.Http;
using Newtonsoft.Json.Linq;


namespace DiscordBot.Modules
{
  public class MemeReddit : ModuleBase
  {
    [Command("meme")]
    [Alias("reddit")]
    public async Task Meme(string subreddit = null)
    {
      var client = new HttpClient();
      var result = await client.GetStringAsync($"https://reddit.com/r/{subreddit ?? "memes"}/random.json?limit=1 ");
      if ( !result.StartsWith("["))
      {
        await Context.Channel.SendMessageAsync("This subreddit doesn't exist!");
        return;
      }
      JArray arr = JArray.Parse(result);
      JObject post = JObject.Parse(arr[0]["data"]["children"][0]["data"].ToString());

      var builder = new EmbedBuilder()
        .WithImageUrl(post["url"].ToString())
        .WithColor(new Color (33, 176, 252))
        .WithTitle(post["title"].ToString())
        .WithUrl("https://reddit.com" + post["permalink"].ToString())
        .WithFooter($"💬 {post["num_comments"]} ⬆️ {post["ups"]} ");
      var embed = builder.Build();
      await Context.Channel.SendMessageAsync(null, false, embed);
    }

  }
}