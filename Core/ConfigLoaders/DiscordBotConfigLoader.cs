using System.Data;
using System.Text;
using DiscordBot.Core.ConfigResults;
using DiscordBot.Core.Intrerfaces;
using Newtonsoft.Json;

namespace DiscordBot.Core.ConfigLoaders;

public class DiscordBotConfigLoader : IConfigLoader
{
    public IConfig Load(string path)
    {
        var buffer = File.ReadAllBytes(path);
        var encodedString = Encoding.UTF8.GetString(buffer);
        
        var config = JsonConvert.DeserializeObject<DiscordBotConfig>(encodedString);
        
        return config ?? throw new NoNullAllowedException("Config load error");
    }
}