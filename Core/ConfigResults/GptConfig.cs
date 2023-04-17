using DiscordBot.Core.Intrerfaces;
using Newtonsoft.Json;

namespace DiscordBot.Core.ConfigResults;

public class GptConfig : IConfig
{
    public string ApiKey { get; set; }
}