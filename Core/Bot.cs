using Discord;
using Discord.WebSocket;
using DiscordBot.Core.Intrerfaces;

namespace DiscordBot.Core;

public class Bot : IBot
{
    private readonly string _token;

    private readonly DiscordSocketClient _client;

    public Bot(string token)
    {
        _token = token;
        _client = new DiscordSocketClient(new DiscordSocketConfig()
        {
            GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent
        });
        _client.LoginAsync(TokenType.Bot, token).Wait();
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
        throw new NotImplementedException();
    }
}