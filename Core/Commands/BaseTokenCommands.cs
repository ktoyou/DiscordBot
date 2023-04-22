using Discord.WebSocket;
using DiscordBot.Core.Intrerfaces;

namespace DiscordBot.Core.Commands;

public class BaseTokenCommands : ICommandHandler
{
    
    
    public async Task HandleAsync(SocketMessage message)
    {
        
    }

    public string GetTextCommand() => "token";
}