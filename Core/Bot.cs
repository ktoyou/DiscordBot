using Discord;
using Discord.WebSocket;
using DiscordBot.Core.ConfigResults;
using DiscordBot.Core.Intrerfaces;
using DiscordBot.Db;
using DiscordBot.Model.Repository;
using Microsoft.Extensions.Logging;

namespace DiscordBot.Core;

public class Bot : IBot
{
    private readonly DiscordBotConfig _config;

    private readonly DiscordSocketClient _client;

    private readonly ILogger<Bot> _logger;

    public Bot(DiscordBotConfig config, ILogger<Bot> logger)
    {
        _config = config;
        _logger = logger;
        
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
            _logger.Log(LogLevel.Information, "Message from discord server");
            await TryDispatchCommand(arg);
        }
    }

    private async Task TryDispatchCommand(SocketMessage arg)
    {
        try
        {
            var splitCommand = arg.Content.Split("///code ");
            _logger.Log(LogLevel.Information, $"Trying dispatch command {splitCommand[1]}");
            var commandDispatcher = new CommandDispatcher(arg);
            await commandDispatcher.DispatchCommandAsync(splitCommand[1]);
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