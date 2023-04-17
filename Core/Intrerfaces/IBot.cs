namespace DiscordBot.Core.Intrerfaces;

public interface IBot
{
    Task RunAsync();

    Task StopAsync();
}