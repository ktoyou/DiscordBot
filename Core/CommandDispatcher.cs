
using Discord.WebSocket;
using DiscordBot.Core.Commands;
using DiscordBot.Core.ConfigLoaders;
using DiscordBot.Core.ConfigResults;
using DiscordBot.Core.Intrerfaces;

namespace DiscordBot.Core;

public class CommandDispatcher : ICommandDispatcher
{
    private readonly Dictionary<string, ICommandHandler> _commands;

    public CommandDispatcher()
    {
        var openAiConfig = new GptConfigLoader().Load("gpt.json") as GptConfig;
        _commands = new Dictionary<string, ICommandHandler>()
        {
            {
                "gpt",
                new GptCommandAsync(openAiConfig.ApiKey)
            },
            {
                "register",
                new RegisterUserCommandAsync()
            },
            {
                "help",
                new HelpCommandAsync()
            },
            {
                "image",
                new GenerateImageCommandAsync(openAiConfig.ApiKey)
            }
        };
    }

    public async Task DispatchCommandAsync(SocketMessage socketMessage)
    {
        var splitInput = socketMessage.Content.Split(' ');
        var commandName = splitInput[1];

        if (!_commands.ContainsKey(commandName))
        {
            var error = new NotFoundCommandAsync(commandName);
            await error.HandleAsync(socketMessage);
            return;
        }
        
        await _commands[commandName].HandleAsync(socketMessage);
    }
}