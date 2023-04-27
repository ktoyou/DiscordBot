using Discord.WebSocket;
using DiscordBot.Core.Intrerfaces;

namespace DiscordBot.Core.Commands;

public class HelpCommandAsync : ICommandHandler
{
    private static string _helpMessage = ":one:  ///code gpt [message] - данная команда отправляет запрос к ChatGPT, после чего получает ответ и направляет его в текстовый канал. Вместо [message] укажите Ваше сообщение.\n" +
                                         ":two:  ///code register - регистрирует пользователя в боте для работы с другими командами.\n" +
                                         "P.S список команд будет расширяться :) Спасибо что зашли на наш сервер :heart:";
    
    public async Task HandleAsync(SocketMessage message) 
        => await message.Channel.SendMessageAsync(_helpMessage);
}