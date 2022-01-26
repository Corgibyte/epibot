## EpiBot Discord Bot

By Kate Kiatsiri, Aaron Minnick, Jeff Terrell, Colt Thatcher, and Hannah Young

A [Discord](https://discord.com) bot that provides various functionality to assist [Epicodus](https://www.epicodus.com) students and instructors.

### Technologies Used

- C#
- .NET
- MySQL
- Entity Framework
- Discord.NET

### Description

This is a Discord bot utilizing the (Discord.NET)[https://discordnet.dev/index.html] library. It provides a variety of functions to assist Epicodus students and instructors. Full feature list below.

---

## Setup Your Own Bot

#### Requirements

* [git](https://git-scm.com)
* [.NET](https://dotnet.microsoft.com/en-us/)
* [MySQL](https://www.mysql.com/)

#### Generate bot token and permissions

TODO

#### To start bot

1. Download and install the [.NET 5.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/5.0) as required for your system. Be sure to add the .NET sdk to your PATH
2. Use terminal to navigate to desired parent directory and use `git clone https://github.com/Corgibyte/epibot.git EpiBot.Solution`
3. Navigate into the project directory nested inside the .Solution directory: `cd EpiBot.Solution/EpiBot`
4. Create an appsettings.json file: `touch appsettings.json`
5. Edit the new appsettings.json file and add the following, making sure to replace the indicated sections with your MySQL user ID and password:
```
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;database=epibot;uid=[YOUR MYSQL USER ID];pwd=[YOUR MYSQL PASSWORD];"
  },
  "DiscordToken": "[YOUR DISCORD TOKEN]"
}
```
6. Back in the terminal, in the EpiBot directory build the project: `dotnet build`
7. Create database from migration data: `dotnet ef database update`
8. Run project: `dotnet run`

### Interacting with the bot

TODO

--------------------

## Commands

TODO

--------------------

### Known bugs:

* None

### License

[Hippocratic License 3.0](https://github.com/Corgibyte/epibot/blob/main/LICENSE.md), Copyright 2022 Kate Kiatsiri, Aaron Minnick, Jeff Terrell, Colt Thatcher, and Hannah Young.
