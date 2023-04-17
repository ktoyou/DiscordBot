namespace DiscordBot.Core.Intrerfaces;

public interface IConfigLoader
{
    IConfig Load(string path);
}