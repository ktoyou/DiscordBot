using DiscordBot.Core.ConfigLoaders;
using DiscordBot.Core.ConfigResults;
using DiscordBot.Db;
using Microsoft.EntityFrameworkCore.Design;

namespace DiscordBot.Factory;

public class SqliteApplicationContextFactory : IDesignTimeDbContextFactory<SqliteApplicationContext>
{
    public SqliteApplicationContext CreateDbContext(string[] args)
    {
        var sqliteConfigLoader = new SqliteConfigLoader();
        var sqliteConfig = sqliteConfigLoader.Load("db.json") as SqliteConfig;

        return new SqliteApplicationContext(sqliteConfig);
    }
}