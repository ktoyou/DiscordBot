using Discord;
using Discord.WebSocket;
using DiscordBot.Core.ConfigResults;
using DiscordBot.Core.Intrerfaces;
using DiscordBot.Db;
using DiscordBot.Model.Repository;

namespace DiscordBot.Core;

public class Bot : IBot
{
    private readonly DiscordBotConfig _config;

    private readonly DiscordSocketClient _client;

    public Bot(DiscordBotConfig config)
    {
        _config = config;
        _client = new DiscordSocketClient(new DiscordSocketConfig()
        {
            GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent
        });
        _client.LoginAsync(TokenType.Bot, config.Token).Wait();
        _client.MessageReceived += ClientOnMessageReceived;
    }

    private async Task ClientOnMessageReceived(SocketMessage arg)
    {
        if (arg.Content.StartsWith("///code"))
        {
            var splitCommand = arg.Content.Split("///code ");
            var commandDispatcher = new CommandDispatcher(arg);
            await commandDispatcher.DispatchCommandAsync(splitCommand[1]);
        }
    }

    public async Task RunAsync()
    {
        await _client.StartAsync();
    }

    public async Task StopAsync()
    {
        await _client.StopAsync();
    }
}