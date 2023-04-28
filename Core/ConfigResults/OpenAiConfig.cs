using DiscordBot.Core.Intrerfaces;
using Newtonsoft.Json;

namespace DiscordBot.Core.ConfigResults;

public class OpenAiConfig : IConfig
{
    public string ApiKey { get; set; }
}