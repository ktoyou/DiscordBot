using System.Data;
using System.Text;
using Discord.Commands;
using DiscordBot.Core.ConfigResults;
using DiscordBot.Core.Intrerfaces;
using Newtonsoft.Json;

namespace DiscordBot.Core.ConfigLoaders;

public class OpenAiConfigLoader : IConfigLoader
{
    public IConfig Load(string path)
    {
        var buffer = File.ReadAllBytes(path);
        var encodedString = Encoding.UTF8.GetString(buffer);
        
        var config = JsonConvert.DeserializeObject<OpenAiConfig>(encodedString);
        
        return config ?? throw new NoNullAllowedException("Config load error");
    }
}