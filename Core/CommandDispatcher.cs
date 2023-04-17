
using Discord.WebSocket;
using DiscordBot.Core.Commands;
using DiscordBot.Core.ConfigLoaders;
using DiscordBot.Core.ConfigResults;
using DiscordBot.Core.Intrerfaces;

namespace DiscordBot.Core;

public class CommandDispatcher : ICommandDispatcher
{
    private static readonly Dictionary<string, ICommandHandler> _commands = new Dictionary<string, ICommandHandler>()
    {
        {
            "gpt",
            new GptCommandAsync((new GptConfigLoader().Load("gpt.json") as GptConfig).ApiKey)
        }
    };

    private readonly SocketMessage _socketMessage;

    public CommandDispatcher(SocketMessage message)
    {
        _socketMessage = message;
    }

    public async Task DispatchCommandAsync(string command)
    {
        var splitInput = command.Split(' ');
        var commandName = splitInput[0];

        if (!_commands.ContainsKey(commandName))
        {
            var error = new NotFoundCommandAsync(commandName);
            await error.HandleAsync(_socketMessage);
            return;
        }
        
        await _commands[commandName].HandleAsync(_socketMessage);
    }
}