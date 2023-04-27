using DiscordBot.Db;
using DiscordBot.Model.Db.Entities;
using Microsoft.EntityFrameworkCore;

namespace DiscordBot.Model.Repository;

public class UsersRepository : AbstractRepository<User>
{
    public UsersRepository(SqliteApplicationContext sqliteApplicationContext) : base(sqliteApplicationContext) { }

    public override async Task<User> GetByIdAsync(int id) 
        => await SqliteApplicationContext.Users.FirstOrDefaultAsync(u => u.Id == id);

    public override async Task<IEnumerable<User>> GetAllAsync() 
        => await SqliteApplicationContext.Users.ToListAsync();

    public override async Task AddAsync(User item)
    {
        await SqliteApplicationContext.Users.AddAsync(item);
        await SqliteApplicationContext.SaveChangesAsync();
    }

    public override async Task RemoveAsync(User item)
    {
        SqliteApplicationContext.Users.Remove(item);
        await SqliteApplicationContext.SaveChangesAsync();
    }

    public override async Task EditAsync(User item) 
        => await SqliteApplicationContext.SaveChangesAsync();

    public async Task<User> GetByDiscordIdAsync(ulong id) 
        => await SqliteApplicationContext.Users.FirstOrDefaultAsync(u => u.DiscordId == id);
}