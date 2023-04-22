using Discord.WebSocket;
using DiscordBot.Core.Intrerfaces;

namespace DiscordBot.Core.Commands;

public class NotFoundCommandAsync : ICommandHandler
{
    private readonly string _command;
    
    public NotFoundCommandAsync(string command)
    {
        _command = command;
    }
    
    public async Task HandleAsync(SocketMessage message) => 
        await message.Channel.SendMessageAsync($"Команда {_command} не найдена.");

    public string GetTextCommand() => string.Empty;
}