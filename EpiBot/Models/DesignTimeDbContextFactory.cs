using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;



//? I'm not sure whether this is actually necessary.
//? The program works fine without the entire file, but I'm unsure if there will be
//? consequences with the migrations and such
namespace EpiBot.Models
{
  public class EpiBotContextFactory : IDesignTimeDbContextFactory<EpiBotContext>
  {
    EpiBotContext IDesignTimeDbContextFactory<EpiBotContext>.CreateDbContext(string[] args)
    {
      IConfigurationRoot configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();
      var builder = new DbContextOptionsBuilder<EpiBotContext>();

      builder.UseMySql(configuration["ConnectionStrings:DefaultConnection"], ServerVersion.AutoDetect(configuration["ConnectionStrings:DefaultConnection"]));
      return new EpiBotContext(builder.Options);
    }
  }
}