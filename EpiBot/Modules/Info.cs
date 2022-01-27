using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using DiscordBot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Modules
{

  public class InfoModule : InteractionModuleBase<SocketInteractionContext>
  {

    [SlashCommand("userinfo", "Responds with user's info or a specified user's info")]
    public async Task UserInfo(SocketGuildUser user = null)
    {
      if (user == null)
      {
        var builder = new EmbedBuilder()
          .WithThumbnailUrl(Context.User.GetAvatarUrl() ?? Context.User.GetDefaultAvatarUrl())
          .WithDescription("Your Discord Information")
          .WithColor(new Color(0, 184, 177))
          .AddField("User ID", Context.User.Id, true)
          .AddField("Account Created on", Context.User.CreatedAt.ToString("MM/dd/yyyy"), true)
          .AddField("Joined this server on", (Context.User as SocketGuildUser).JoinedAt.Value.ToString("MM/dd/yyyy"), true)
          .AddField("Roles", string.Join(" ", (Context.User as SocketGuildUser).Roles.Select(x => x.Mention)))
          .WithCurrentTimestamp();

          var embed = builder.Build();
          await RespondAsync(embed : embed);
      }
      else
      {
        var builder = new EmbedBuilder()
        .WithThumbnailUrl(user.GetAvatarUrl() ?? user.GetDefaultAvatarUrl())
        .WithDescription("Your Discord Information")
        .WithColor(new Color(0, 184, 177))
        .AddField("User ID", user.Id, true)
        .AddField("Account Created on", user.CreatedAt.ToString("MM/dd/yyyy"), true)
        .AddField("Joined this server on", (user as SocketGuildUser).JoinedAt.Value.ToString("MM/dd/yyyy"), true)
        .AddField("Roles", string.Join(" ", (user as SocketGuildUser).Roles.Select(x => x.Mention)))
        .WithCurrentTimestamp();

        var embed = builder.Build();
        await RespondAsync(embed : embed);
      }
    }
    
    [SlashCommand("serverinfo", "Returns information about the server")] 

    public async Task ServerInfo()
    {
      var builder = new EmbedBuilder()
        .WithThumbnailUrl(Context.Guild.IconUrl)
        .WithTitle($"{Context.Guild.Name} Information")
        .WithColor(new Color(0, 184, 177))
        .AddField("Create on", Context.Guild.CreatedAt.ToString("MM/dd/yyyy"), true)
        .AddField("Membercount", (Context.Guild as SocketGuild).MemberCount + " member" , true )
        .AddField("Online Users", (Context.Guild as SocketGuild).Users.Where(x => x.Status != UserStatus.Offline).Count() + " members", true);

      var embed = builder.Build();
      await RespondAsync(embed : embed);
    }
  }  
}