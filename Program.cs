using DiscordBot.Core;
using DiscordBot.Core.ConfigLoaders;
using DiscordBot.Core.ConfigResults;

var discordBotConfigLoader = new DiscordBotConfigLoader();
var config = discordBotConfigLoader.Load("bot.json") as DiscordBotConfig;

var bot = new Bot(config.Token);
await bot.RunAsync();

Console.ReadKey();