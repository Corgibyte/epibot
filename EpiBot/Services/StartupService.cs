using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace EpiBot.Services
{
  public class StartupService
  {
    private readonly IServiceProvider _provider;
    private readonly DiscordSocketClient _discord;
    private readonly CommandService _commands;
    private readonly InteractionService _interactions;
    private readonly IConfigurationRoot _config;

    public StartupService(
      IServiceProvider provider,
      DiscordSocketClient discord,
      CommandService commands,
      InteractionService interactions,
      IConfigurationRoot config
    )
    {
      _provider = provider;
      _discord = discord;
      _commands = commands;
      _interactions = interactions;
      _config = config;
    }

    public async Task StartAsync()
    {
      //TODO: change this
      string discordToken = _config["DiscordToken"];
      if (string.IsNullOrWhiteSpace(discordToken))
      {
        throw new Exception("Token error");
      }

      _discord.Ready += ReadyAsync;

      // login and launch bot
      await _discord.LoginAsync(TokenType.Bot, discordToken);
      await _discord.StartAsync();

      // register all text command modules
      await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _provider);
      // register all interaction (i.e. slash command) modules
      await _interactions.AddModulesAsync(Assembly.GetEntryAssembly(), _provider);
    }

    private async Task ReadyAsync()
    {
      //testing: setting command on specific guild - below arg may be got programatically?
      await _interactions.RegisterCommandsToGuildAsync(935181165755764826);
      //deployment: use below to set command globally (anywhere the bot is used)
      // await _interactions.RegisterCommandsGloballyAsync(true);
    }
  }
}