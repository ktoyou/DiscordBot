using DiscordBot.Core;
using DiscordBot.Core.ConfigLoaders;
using DiscordBot.Core.ConfigResults;
using Microsoft.Extensions.Logging;

var discordBotConfigLoader = new DiscordBotConfigLoader();
var discordBotConfig = discordBotConfigLoader.Load("bot.json") as DiscordBotConfig;
if (discordBotConfig == null) throw new Exception("Failed to load bot.json");

var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole();
});

var bot = new Bot(discordBotConfig, loggerFactory.CreateLogger<Bot>(), new CommandDispatcher());
await bot.RunAsync();

Console.ReadKey();