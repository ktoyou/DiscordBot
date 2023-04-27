using Discord;
using Discord.WebSocket;
using DiscordBot.Core.Intrerfaces;
using OpenAI_API;

namespace DiscordBot.Core.Commands;

public class GenerateImageCommandAsync : ICommandHandler
{
    private readonly OpenAIAPI _openAiApi;
    
    public GenerateImageCommandAsync(string apiKey)
    {
        _openAiApi = new OpenAIAPI(new APIAuthentication(apiKey));
    }
    
    public async Task HandleAsync(SocketMessage message)
    {
        var splited = message.Content.Split("///code image ");
        var prompt = splited[1];

        var imageResult = await _openAiApi.ImageGenerations.CreateImageAsync(prompt);
        var imageUrl = imageResult.Data.FirstOrDefault().Url;

        var httpClient = new HttpClient();
        var responseMessage = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, imageUrl));
        await message.Channel.SendFileAsync(new FileAttachment(responseMessage.Content.ReadAsStream(), "image.png"));
    }
}