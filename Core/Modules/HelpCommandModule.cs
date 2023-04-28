using System.Text;
using Discord;
using Discord.Commands;
using DiscordBot.Core.Intrerfaces;
using DiscordBot.Core.Mappers;

namespace DiscordBot.Core.Modules;

public class HelpCommandModule : ModuleBase<SocketCommandContext>, IGetMessageReference
{
    public MessageReference MessageReference => new MessageReference(Context.Message.Id, Context.Channel.Id, Context.Guild.Id);
    
    private readonly CommandService _commandService;

    public HelpCommandModule(CommandService commandService)
    {
        _commandService = commandService;
    }
    
    [Command("help", RunMode = RunMode.Async)]
    [Summary("Выводит список всех команд с их описанием.")]
    public async Task HelpAsync()
    {
        var helpResponse = new StringBuilder();
        var modules = _commandService.Modules;
        var mapper = new NumbersToDiscordEmojiMapper();
        var currentCommandNumber = 0;
        foreach (var module in modules)
        {
            foreach (var command in module.Commands)
            {
                currentCommandNumber++;
                var mappedNumber = mapper.MapNumber(currentCommandNumber);
                var summary = command.Summary;

                if (summary == null)
                {
                    helpResponse.Append($"{mappedNumber}\t{command.Name} - нет описания\n");
                    continue;
                }

                helpResponse.Append($"{mappedNumber}\t{command.Name} - {command.Summary}\n");
            }
        }

        await ReplyAsync(helpResponse.ToString(), messageReference: MessageReference);
    }
}