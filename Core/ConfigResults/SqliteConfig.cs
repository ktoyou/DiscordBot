using DiscordBot.Core.Intrerfaces;

namespace DiscordBot.Core.ConfigResults;

public class SqliteConfig : IConfig
{
    public string DataSource { get; set; }

    public string Version { get; set; }
}