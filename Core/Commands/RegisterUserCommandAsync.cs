using Discord.WebSocket;
using DiscordBot.Core.Intrerfaces;
using DiscordBot.Model.Db.Entities;
using DiscordBot.Model.Repository;

namespace DiscordBot.Core.Commands;

public class RegisterUserCommandAsync : ICommandHandler
{
    public async Task HandleAsync(SocketMessage message)
    {
        var repository = ServicesContainer.GetRepository<User>() as UsersRepository;

        var isRegistered = (await repository.GetByDiscordIdAsync(message.Author.Id)) != null;
        if (isRegistered)
        {
            await message.Channel.SendMessageAsync($@"Пользователь {message.Author.Username} существует!");
            return;
        }

        var user = new User()
        {
            DiscordId = message.Author.Id,
            UserName = message.Author.Username,
            Tokens = 0
        };

        await repository.AddAsync(user);
        await message.Channel.SendMessageAsync($@"Пользователь {user.UserName} зарегистрирован!");
    }
}