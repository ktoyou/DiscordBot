using Discord;
using Discord.Commands;
using DiscordBot.Core.Intrerfaces;
using DiscordBot.Model.Db.Entities;
using DiscordBot.Model.Repository;

namespace DiscordBot.Core.Modules;

public class UserCommandsModule : ModuleBase<SocketCommandContext>, IGetMessageReference
{
    public MessageReference MessageReference => new MessageReference(Context.Message.Id, Context.Channel.Id, Context.Guild.Id);

    private readonly UsersRepository _usersRepository;
    
    public UserCommandsModule(UsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    [Command("register", RunMode = RunMode.Async)]
    [Summary("Регистрирует пользователя на сервере, для использования команд которые требуют регистрации.")]
    public async Task RegisterUserAsync()
    {
        var isRegistered = await _usersRepository.GetByDiscordIdAsync(Context.User.Id) != null;
        if (isRegistered)
        {
            await Context.Channel.SendMessageAsync($@"Вы зарегистрированы!", allowedMentions: AllowedMentions.None, messageReference: MessageReference);
            return;
        }

        var user = new User()
        {
            DiscordId = Context.User.Id,
            UserName = Context.User.Username,
            Tokens = 0
        };

        await _usersRepository.AddAsync(user);
        await Context.Channel.SendMessageAsync($@"Регистрация прошла успешно.", allowedMentions: AllowedMentions.None, messageReference: MessageReference);
    }
}