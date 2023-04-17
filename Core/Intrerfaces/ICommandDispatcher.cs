using System.Windows.Input;

namespace DiscordBot.Core.Intrerfaces;

public interface ICommandDispatcher
{
     Task DispatchCommandAsync(string command);
}