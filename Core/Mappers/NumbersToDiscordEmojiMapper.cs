using System.Text;

namespace DiscordBot.Core.Mappers;

public class NumbersToDiscordEmojiMapper
{
    private readonly Dictionary<int, string> _numbersAssotiationWithDiscordEmoji;

    public NumbersToDiscordEmojiMapper()
    {
        _numbersAssotiationWithDiscordEmoji = new Dictionary<int, string>()
        {
            { 0, ":zero: "},
            { 1, ":one:" },
            { 2, ":two:" },
            { 3, ":three:" },
            { 4, ":four:" },
            { 5, ":five:" },
            { 6, ":six:" },
            { 7, ":seven:" },
            { 8, ":eight:" },
            { 9, ":nine:" }
        };
    }

    public string MapNumber(int number)
    {
        var stringNumber = number.ToString();
        var output = new StringBuilder();
        
        for (var i = 0; i < stringNumber.Length; i++)
        {
            var findEmoji = _numbersAssotiationWithDiscordEmoji.First(e => e.Key == stringNumber[i] - '0');
            output.Append(findEmoji.Value);
        }
        
        return output.ToString();
    }
}