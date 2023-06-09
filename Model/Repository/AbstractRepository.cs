using DiscordBot.Db;
using DiscordBot.Model.Repository.Interfaces;

namespace DiscordBot.Model.Repository;

public abstract class AbstractRepository<T> : IRepository<T>
{
    protected readonly SqliteApplicationContext SqliteApplicationContext;
    
    protected AbstractRepository(SqliteApplicationContext sqliteApplicationContext)
    {
        SqliteApplicationContext = sqliteApplicationContext;
    }

    public abstract Task<T> GetByIdAsync(int id);

    public abstract Task<IEnumerable<T>> GetAllAsync();

    public abstract Task AddAsync(T item);

    public abstract Task RemoveAsync(T item);

    public abstract Task EditAsync(T item);
}