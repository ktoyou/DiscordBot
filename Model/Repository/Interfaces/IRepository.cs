namespace DiscordBot.Model.Repository.Interfaces;

public interface IRepository<T>
{
    Task<T> GetByIdAsync(int id);

    Task<IEnumerable<T>> GetAllAsync();

    Task AddAsync(T item);

    Task RemoveAsync(T item);

    Task EditAsync(T item);
}