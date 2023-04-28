using Discord;
using Discord.Commands;
using DiscordBot.Core.ConfigResults;
using DiscordBot.Core.Intrerfaces;
using OpenAI_API;

namespace DiscordBot.Core.Modules;

public class OpenAiCommandsModule : ModuleBase<SocketCommandContext>, IGetMessageReference
{
    public MessageReference MessageReference => new MessageReference(Context.Message.Id, Context.Channel.Id, Context.Guild.Id);

    private readonly OpenAIAPI _openAiApi;
    
    public OpenAiCommandsModule(OpenAiConfig config)
    {
        _openAiApi = new OpenAIAPI(config.ApiKey);
    }
    
    [Command("gpt", RunMode = RunMode.Async)]
    [Summary("Отправляет запрос к ChatGPT, и отвечает в текущем дискорд канале.")]
    public async Task GptRequestAsync(string prompt)
    {
        var completionResult = await _openAiApi.Completions.CreateCompletionAsync(prompt, null, 2000);
        await ReplyAsync(completionResult.Completions[0].Text, messageReference: MessageReference);
    }

    [Command("image")]
    [Summary("Генерирует изображение на основе вашего описания.")]
    public async Task GenerateImageAsync(string prompt)
    {
        var imageResult = await _openAiApi.ImageGenerations.CreateImageAsync(prompt);
        var imageUrl = imageResult.Data.FirstOrDefault().Url;

        var httpClient = new HttpClient();
        var responseMessage = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, imageUrl));
        await Context.Channel.SendFileAsync(new FileAttachment(await responseMessage.Content.ReadAsStreamAsync(), "image.png"), messageReference: MessageReference);
    }
}