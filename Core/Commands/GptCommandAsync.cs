using Discord.WebSocket;
using DiscordBot.Core.Intrerfaces;
using OpenAI_API;

namespace DiscordBot.Core.Commands;

public class GptCommandAsync : ICommandHandler
{
    private readonly OpenAIAPI _openAiApi;

    public GptCommandAsync(string apiKey)
    {
        _openAiApi = new OpenAIAPI(new APIAuthentication(apiKey));
    }
    
    public async Task HandleAsync(SocketMessage message)
    {
        var prompt = message.Content.Split("///code gpt"); // Todo: Данную говнострочку нужно будет убрать, а так пока временно 
        var completionResult = await _openAiApi.Completions.CreateCompletionAsync(prompt[1], null, 2000);
        await message.Channel.SendMessageAsync(completionResult.Completions[0].Text, false, null, null, null, message.Reference);
    }
}