using DiscordBot.Core.ConfigLoaders;
using DiscordBot.Core.ConfigResults;
using DiscordBot.Db;
using DiscordBot.Model.Db.Entities;
using DiscordBot.Model.Repository;
using DiscordBot.Model.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DiscordBot.Core;

public class ServicesContainer
{
    public static IServiceProvider Provider { get; }

    static ServicesContainer()
    {
        var sqliteConfigLoader = new SqliteConfigLoader();
        var sqliteConfig = sqliteConfigLoader.Load("db.json") as SqliteConfig;
        if (sqliteConfig == null) throw new Exception("Failed to load db.json");
        
        var serviceCollection = new ServiceCollection();
        
        serviceCollection.AddSingleton(sqliteConfig);
        serviceCollection.AddDbContext<SqliteApplicationContext>();
        serviceCollection.AddTransient<IRepository<User>, UsersRepository>();
        
        Provider = serviceCollection.BuildServiceProvider();
    }

    public static IRepository<T>? GetRepository<T>() => Provider.GetService<IRepository<T>>();
}