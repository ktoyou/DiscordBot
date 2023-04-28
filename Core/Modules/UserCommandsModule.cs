using System.Text;
using Discord;
using Discord.Commands;
using DiscordBot.Core.Intrerfaces;
using DiscordBot.Core.Mappers;
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
        if (await GetRegisteredUserAsync() != null)
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

    [Command("tokens", RunMode = RunMode.Async)]
    [Summary("Выводит текущий баланс.")]
    public async Task TokensAsync()
    {
        var user = await SendErrorMessageIfUserNotRegisteredAsync();
        if(user == null) return;
        
        var profileInfoStringBuilder = new StringBuilder();
        var mapper = new NumbersToDiscordEmojiMapper();
        profileInfoStringBuilder.Append($"Ваши токены: {mapper.MapNumber(user.Tokens)}");

        await ReplyAsync(profileInfoStringBuilder.ToString(), allowedMentions: AllowedMentions.None, messageReference: MessageReference);
    }

    private async Task<User?> SendErrorMessageIfUserNotRegisteredAsync()
    {
        var user = await GetRegisteredUserAsync();
        if (user == null)
            await Context.Channel.SendMessageAsync($@"Вашего профиля не существует. Используйте команду для регистрации.", allowedMentions: AllowedMentions.None, messageReference: MessageReference);
        return user;
    }

    private async Task<User?> GetRegisteredUserAsync() => await _usersRepository.GetByDiscordIdAsync(Context.User.Id);
}