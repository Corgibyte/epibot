using Discord.Interactions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace EpiBot.Modules
{
  //slash command must be public, and must implement InteractionModuleBase

  //example: 8 ball (from tutorial)
  public class SlashCommands : InteractionModuleBase<SocketInteractionContext>
  {
    // our first /command!
    [SlashCommand("8ball", "find your answer!")]
    public async Task EightBall(string question)
    {
      // create a list of possible replies
      var replies = new List<string>();

      // add our possible replies
      replies.Add("yes");
      replies.Add("no");
      replies.Add("maybe");
      replies.Add("hazzzzy....");

      // get the answer
      var answer = replies[new Random().Next(replies.Count - 1)];

      // reply with the answer
      await RespondAsync($"You asked: [**{question}**], and your answer is: [**{answer}**]");
    }
  }
}