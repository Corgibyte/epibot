using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace EpiBot.Modules
{
  public class RandomPairs : InteractionModuleBase<SocketInteractionContext>
  {
    private Random _rand;
    private DiscordSocketClient _discord;

    public RandomPairs(IServiceProvider services)
    {
      _rand = services.GetRequiredService<Random>();
      _discord = services.GetRequiredService<DiscordSocketClient>();
      //listen for button click
      _discord.ButtonExecuted += RepeatButtonHandler;
    }

    [SlashCommand("pairs", "Make random pairs from a list of space-separated names")]
    public async Task Pairs(string listOfNames)
    {
      var button = new Discord.ComponentBuilder()
        .WithButton("Repeat Shuffle", "shuffle");
      await RespondAsync(text: MakeContentText(MakePairs(listOfNames.Split())), components: button.Build());
    }

    private string MakeContentText(string[] pairs)
    {
      string text = "";
      foreach(string s in pairs)
      {
        text += s + "\n";
      }
      return text;
    }

    private string[] MakePairs(string[] names)
    {
      //shuffle
      int i = names.Length;
      while (i > 1)
      {
        int j = _rand.Next(i--);
        string temp = names[i];
        names[i] = names[j];
        names[j] = temp;
      }

      //make pairs
      int half = names.Length/2;
      string[] pairs = new string[half];
      for (int k = 0; k < half; k++)
      {
        pairs[k] = names[k] + ", " + names[k+half];
      }
      if (names.Length % 2 == 1) pairs[0] += ", " + names[names.Length-1];

      return pairs;
    }

    private async Task RepeatButtonHandler(SocketMessageComponent comp)
    {
      if (comp.Data.CustomId == "shuffle")
      {
        await comp.UpdateAsync(x => 
        {
        x.Content = MakeContentText(MakePairs(ParseNamesFromContent(comp.Message.Content)));
        });
      }
    }

    private string[] ParseNamesFromContent(string content)
    {
      content = content.Replace(",", "");
      string[] names = content.Split();
      return names;
    }
  }
}