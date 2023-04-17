using Discord.WebSocket;

namespace DiscordBot.Core.Intrerfaces;

public interface ICommandHandler
{
    Task HandleAsync(SocketMessage message);
}