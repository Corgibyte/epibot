using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

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