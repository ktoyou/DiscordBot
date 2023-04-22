using DiscordBot.Db;
using DiscordBot.Model.Db.Entities;
using Microsoft.EntityFrameworkCore;

namespace DiscordBot.Model.Repository;

public class UsersRepository : AbstractRepository<User>
{
    public UsersRepository(SqliteApplicationContext sqliteApplicationContext) : base(sqliteApplicationContext) { }

    public override async Task<User> GetByIdAsync(int id) => await TryGetByIdAsync(id);
    
    protected override async Task<User> TryGetByIdAsync(int id)
    {
        try
        {
            return await SqliteApplicationContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return null;
    }

    public override async Task<IEnumerable<User>> GetAllAsync() => await TryGetAllAsync();

    protected override async Task<IEnumerable<User>> TryGetAllAsync()
    {
        try
        {
            return await SqliteApplicationContext.Users.ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return null;
    }

    public override async Task AddAsync(User item) => await TryAddAsync(item);

    protected override async Task TryAddAsync(User item)
    {
        try
        {
            await SqliteApplicationContext.Users.AddAsync(item);
            await SqliteApplicationContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public override async Task RemoveAsync(User item) => await TryRemoveAsync(item);

    protected override async Task TryRemoveAsync(User item)
    {
        try
        {
            SqliteApplicationContext.Users.Remove(item);
            await SqliteApplicationContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public override async Task EditAsync(User item) => await TryEditAsync(item);

    protected override async Task TryEditAsync(User item)
    {
        try
        {
            await SqliteApplicationContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public async Task<User> GetByDiscordIdAsync(ulong id) => await TryGetByDiscordIdAsync(id);

    private async Task<User> TryGetByDiscordIdAsync(ulong id)
    {
        try
        {
            return await SqliteApplicationContext.Users.FirstOrDefaultAsync(u => u.DiscordId == id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return null;
    }
}