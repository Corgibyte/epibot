using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace EpiBot
{
  public class StartupService
  {
    private readonly IServiceProvider _provider;
    private readonly DiscordSocketClient _discord;
    private readonly CommandService _commands;
    private readonly IConfigurationRoot _config;

    public StartupService(
      IServiceProvider provider,
      DiscordSocketClient discord,
      CommandService commands,
      IConfigurationRoot config
    )
    {
      _provider = provider;
      _discord = discord;
      _commands = commands;
      _config = config;
    }

    public async Task StartAsync()
    {
      //TODO: change this
      string discordToken = File.ReadAllText("token.txt");
      if (string.IsNullOrWhiteSpace(discordToken))
      {
        throw new Exception("Token error");
      }

      await _discord.LoginAsync(TokenType.Bot, discordToken);
      await _discord.StartAsync();
      await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _provider);
    }
  }
}