using System.Windows.Input;
using Discord.WebSocket;

namespace DiscordBot.Core.Intrerfaces;

public interface ICommandDispatcher
{
     Task DispatchCommandAsync(SocketMessage socketMessage);
}