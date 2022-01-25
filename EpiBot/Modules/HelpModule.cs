using Discord;
using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EpiBot.Modules
{
  public class HelpModule : ModuleBase<SocketCommandContext>
  {
    private readonly CommandService _service;
    private readonly IConfigurationRoot _config;

    public HelpModule(CommandService service, IConfigurationRoot config)
    {
      _service = service;
      _config = config;
    }

    [Command("help")]
    [Summary("Get list of all commands")]
    public async Task HelpAsync()
    {
      string prefix = _config["prefix"];

      List<EmbedBuilder> embeds = new List<EmbedBuilder>();
      foreach (ModuleInfo module in _service.Modules)
      {
        foreach (CommandInfo cmd in module.Commands)
        {
          string summary = cmd.Summary ?? "*No summary given*";

          //Embed builder for each command
          var builder = new EmbedBuilder()
            .WithAuthor(Context.Client.CurrentUser)
            .WithColor(Color.Teal)
            .WithDescription($"*{summary}*");
          string cmdWithParams = cmd.Name;

          //Add parameters
          foreach (ParameterInfo param in cmd.Parameters)
          {
            string paramSummary = param.Summary ?? "*No summary given*";
            cmdWithParams += $" {param.Name}";
            builder.AddField(param.Name, paramSummary);
          }
          builder = builder.WithTitle($"`{cmdWithParams}`");
          embeds.Add(builder);
        }
      }

      //Reply with each embed
      foreach (EmbedBuilder embed in embeds)
      {
        await ReplyAsync("", false, embed.Build());
      }
    }
  }
}