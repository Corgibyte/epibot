using Discord.Interactions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace EpiBot.Modules
{
  public class RandomPairs : InteractionModuleBase<SocketInteractionContext>
  {
    private Random rand;
    public RandomPairs(IServiceProvider services)
    {
      rand = services.GetRequiredService<Random>();
    }

    [SlashCommand("pairs", "Make random pairs from a list of space-separated names")]
    public async Task Pairs(string listOfPairs)
    {
      MakePairs(listOfPairs.Split());
      await RespondAsync("placeholder response");
    }

    private string[] MakePairs(string[] names)
    {
      int i = names.Length;
      while (i > 1)
      {
        int j = rand.Next(i--);
        string temp = names[i];
        names[i] = names[j];
        names[j] = temp;
      }

      int half = names.Length/2;
      string[] pairs = new string[half];
      for (int k = 0; k < half; k++)
      {
        pairs[k] = names[k] + " " + names[k+half];
      }
      if (names.Length % 2 == 1) pairs[0] += " " + names[names.Length-1];

      return pairs;
    }
  }
}