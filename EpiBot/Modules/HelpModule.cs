using Discord;
using Discord.Commands;
using Discord.Interactions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EpiBot.Modules
{
  public class HelpModule : ModuleBase<SocketCommandContext>
  {
    private readonly CommandService _service;
    private readonly IConfigurationRoot _config;
    private readonly IServiceProvider _provider;

    public HelpModule(CommandService service, IConfigurationRoot config, IServiceProvider provider)
    {
      _service = service;
      _config = config;
      _provider = provider;
    }

    [Command("help")]
    [Discord.Commands.Summary("get list of all commands")]
    public async Task HelpAsync()
    {
      string prefix = _config["Prefix"];

      List<EmbedBuilder> embeds = new List<EmbedBuilder>();
      foreach (Discord.Commands.ModuleInfo module in _service.Modules)
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
          builder = builder.WithTitle($"`{prefix}{cmdWithParams}`");
          embeds.Add(builder);
        }
      }

      foreach (Discord.Interactions.SlashCommandInfo slashInfo in _provider.GetService<InteractionService>().SlashCommands)
      {
        string description = slashInfo.Description ?? "*No description given*";

        var builder = new EmbedBuilder()
          .WithAuthor(Context.Client.CurrentUser)
          .WithColor(Color.Green)
          .WithDescription($"*{description}*");
        string slashWithParams = slashInfo.Name;
        foreach (SlashCommandParameterInfo param in slashInfo.Parameters)
        {
          string paramSummary = param.Description ?? "*No description given*";
          slashWithParams += $" {param.Name}";
          builder.AddField(param.Name, paramSummary);
        }
        builder = builder.WithTitle($"`/{slashWithParams}`");
        embeds.Add(builder);
      }

      Embed[] embedArray = new Embed[embeds.Count];
      for (int i = 0; i < embeds.Count; i++)
      {
        embedArray[i] = embeds[i].Build();
      }
      //Reply with each embed
      await ReplyAsync("", embeds: embedArray);
    }
  }
}