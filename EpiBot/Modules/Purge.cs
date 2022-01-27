using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpiBot.Modules
{
  public class PurgeModule : InteractionModuleBase<SocketInteractionContext>
  {
    [SlashCommand("purge", "Delete messages from the most recent based on number given")]
    public async Task Purge(int number)
    {
      if ( number == 0)
      {
        await RespondAsync("No messages have been deleted!!");
      }
      else
      {
        var messages = await Context.Channel.GetMessagesAsync(number).FlattenAsync();
        await (Context.Channel as SocketTextChannel).DeleteMessagesAsync(messages);
        await RespondAsync($"{messages.Count()} messages deleted");
      }
    } 
  } 
}
