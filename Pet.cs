// Model to save to database: Fields: Id, SocketUser
// Register command (like my /byline-register) where it adds to database when user runs command
// Unregister command => same as above except delete
// Query database to find all YourModel in database
// For each YourModel from query, send message to that user with notification
using Discord.WebSocket;
using System.Threading.Tasks;
using Discord.Interactions;
using Microsoft.Extensions.DependencyInjection;
using System;


namespace EpiBot.Models
{
  public class Pet
  {
    public int Id {get; set; }

    private readonly EpiBotContext _db;

    public SocketUser _socketuser;
  }
}



//  public EpiBotContext(DbContextOptions options) : base(options) { }

/*public async Task Pet()
    {
        ulong id = 935182709226098688;
        var channel = _discord.GetChannel(id) as IMessageChannel;
        await channel.SendMessageAsync("New Pet Alert!!!");
    }*/
