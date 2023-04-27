using Discord;
using Discord.WebSocket;
using DiscordBot.Core.ConfigResults;
using DiscordBot.Core.Intrerfaces;
using Microsoft.Extensions.Logging;

namespace DiscordBot.Core;

public class Bot : IBot
{
    private readonly DiscordBotConfig _config;

    private readonly DiscordSocketClient _client;

    private readonly ILogger<Bot> _logger;

    private readonly ICommandDispatcher _commandDispatcher;

    public Bot(DiscordBotConfig config, ILogger<Bot> logger, ICommandDispatcher commandDispatcher)
    {
        _config = config;
        _logger = logger;
        _commandDispatcher = commandDispatcher;
        
        _logger.Log(LogLevel.Information, "Initializing DiscordSocketClient");
        _client = new DiscordSocketClient(new DiscordSocketConfig()
        {
            GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent
        });
        _client.LoginAsync(TokenType.Bot, config.Token).Wait();
        _logger.Log(LogLevel.Information, "DiscordSocketClient initialized");
        
        _client.MessageReceived += ClientOnMessageReceived;
    }

    private async Task ClientOnMessageReceived(SocketMessage arg)
    {
        if (arg.Content.StartsWith("///code"))
        {
            _logger.Log(LogLevel.Information, $"Message from {arg.Channel.Name} discord server");
            await TryDispatchCommand(arg);
        }
    }

    private async Task TryDispatchCommand(SocketMessage arg)
    {
        try
        {
            _logger.Log(LogLevel.Information, $"Trying dispatch command");
            await _commandDispatcher.DispatchCommandAsync(arg);
        }
        catch (Exception e)
        {
            _logger.LogCritical(e.Message);
        }
    }

    public async Task RunAsync()
    {
        _logger.Log(LogLevel.Information, "Bot starting");
        await _client.StartAsync();
    }

    public async Task StopAsync()
    {
        _logger.Log(LogLevel.Information, "Bot stopped");
        await _client.StopAsync();
    }
}