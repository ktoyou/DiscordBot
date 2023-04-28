using System.Reflection;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.Core.ConfigResults;
using DiscordBot.Core.Intrerfaces;
using DiscordBot.Core.Modules;
using Microsoft.Extensions.Logging;

namespace DiscordBot.Core;

public class Bot : IBot
{
    private readonly DiscordBotConfig _config;

    private readonly DiscordSocketClient _client;

    private readonly ILogger<Bot> _logger;

    private readonly CommandService _commandService;

    private readonly IServiceProvider _serviceProvider;

    public Bot(DiscordBotConfig config, IServiceProvider serviceProvider, ILogger<Bot> logger)
    {
        _config = config;
        _logger = logger;
        _serviceProvider = serviceProvider;

        _logger.Log(LogLevel.Information, "Initializing DiscordSocketClient");
        _client = new DiscordSocketClient(new DiscordSocketConfig()
        {
            GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent
        });

        _client.MessageReceived += ClientOnMessageReceived;

        _commandService = new CommandService();
        _commandService.AddModulesAsync(Assembly.GetEntryAssembly(), _serviceProvider).Wait();
    }

    private async Task ClientOnMessageReceived(SocketMessage message)
    {
        if (!(message is SocketUserMessage userMessage) || userMessage.Author.IsBot || !userMessage.Content.StartsWith("!code"))
            return;

        var splitCommand = userMessage.Content.Split("!code ");
        
        var context = new SocketCommandContext(_client, userMessage);
        var result = await _commandService.ExecuteAsync(context, splitCommand[1], _serviceProvider);
        
        if (!result.IsSuccess)
            await context.Channel.SendMessageAsync(result.ErrorReason);
    }

    public async Task RunAsync()
    {
        _logger.Log(LogLevel.Information, "Login on discord server");
        _client.LoginAsync(TokenType.Bot, _config.Token).Wait();
        _logger.Log(LogLevel.Information, "Bot starting");
        await _client.StartAsync();
    }

    public async Task StopAsync()
    {
        _logger.Log(LogLevel.Information, "Bot stopped");
        await _client.StopAsync();
    }
}