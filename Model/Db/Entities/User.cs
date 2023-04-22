namespace DiscordBot.Model.Db.Entities;

public class User
{
    public int Id { get; set; }

    public int Tokens { get; set; }
    
    public string UserName { get; set; }

    public ulong DiscordId { get; set; }
}