using DiscordBot.Core.Intrerfaces;

namespace DiscordBot.Core.ConfigResults;

public class DiscordBotConfig : IConfig
{
    public string Token { get; set; }
}