using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using EpiBot.Models;
using EpiBot.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using System.IO;

namespace EpiBot
{
  public class Startup
  {
    public IConfigurationRoot Configuration { get; }

    public Startup(string[] args)
    {
      var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile(path: "appsettings.json");
      Configuration = builder.Build();
    }

    public static async Task RunAsync(string[] args)
    {
      var startup = new Startup(args);
      await startup.RunAsync();
    }

    public async Task RunAsync()
    {
      var services = new ServiceCollection();
      ConfigureServices(services);
      var provider = services.BuildServiceProvider();
      provider.GetRequiredService<CommandHandler>();
      provider.GetRequiredService<LoggingService>();
      provider.GetRequiredService<ReminderService>();
      await provider.GetRequiredService<StartupService>().StartAsync();
      await Task.Delay(-1);
    }

    private void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<EpiBotContext>(opt =>
                opt.UseMySql(Configuration["ConnectionStrings:DefaultConnection"], ServerVersion.AutoDetect(Configuration["ConnectionStrings:DefaultConnection"])));
      services.AddSingleton(new DiscordSocketClient(new DiscordSocketConfig
      {
        LogLevel = LogSeverity.Verbose,
        MessageCacheSize = 1000
      }))
      .AddSingleton(new CommandService(new CommandServiceConfig
      {                                       // Add the command service to the collection
        LogLevel = LogSeverity.Verbose     // Tell the logger to give Verbose amount of info
        // DefaultRunMode = RunMode.Async,     // Force all commands to run async by default
      }))
      .AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()))
      .AddSingleton<CommandHandler>()         // Add the command handler to the collection
      .AddSingleton<StartupService>()         // Add startupservice to the collection
      .AddSingleton<LoggingService>()         // Add loggingservice to the collection
      .AddSingleton<ReminderService>()
      .AddSingleton<Random>()                 // Add random to the collection
      .AddSingleton(Configuration);           // Add the configuration to the collection
    }
  }
}