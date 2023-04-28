using DiscordBot.Core;
using DiscordBot.Core.ConfigLoaders;
using DiscordBot.Core.ConfigResults;
using DiscordBot.Db;
using DiscordBot.Model.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var discordBotConfigLoader = new DiscordBotConfigLoader();
var discordBotConfig = discordBotConfigLoader.Load("bot.json") as DiscordBotConfig;
if (discordBotConfig == null) throw new Exception("Failed to load bot.json");

var sqliteConfigLoader = new SqliteConfigLoader();
var sqliteConfig = sqliteConfigLoader.Load("db.json") as SqliteConfig;

var openAiConfigLoader = new OpenAiConfigLoader();
var openAiConfig = openAiConfigLoader.Load("openai.json") as OpenAiConfig;

var serviceContainer = new ServiceCollection();
serviceContainer
    .AddSingleton(discordBotConfig)
    .AddSingleton(sqliteConfig)
    .AddSingleton(openAiConfig)
    .AddTransient<SqliteApplicationContext>()
    .AddTransient<UsersRepository>()
    .AddLogging(builder => builder.AddConsole())
    .AddSingleton(e => e.GetService<IServiceProvider>())
    .AddSingleton<Bot>();


var provider = serviceContainer.BuildServiceProvider();
provider.GetService<Bot>()?.RunAsync().Wait();

Console.ReadKey();