using DiscordBot.Core;
using DiscordBot.Core.ConfigLoaders;
using DiscordBot.Core.ConfigResults;

var discordBotConfigLoader = new DiscordBotConfigLoader();
var discordBotConfig = discordBotConfigLoader.Load("bot.json") as DiscordBotConfig;
if (discordBotConfig == null) throw new Exception("Failed to load bot.json");

var bot = new Bot(discordBotConfig);
await bot.RunAsync();

Console.ReadKey();