using DiscordBot.Core.ConfigResults;
using DiscordBot.Core.Intrerfaces;
using DiscordBot.Model.Db.Entities;
using DiscordBot.Model.Db.ModelConfigurators;
using Microsoft.EntityFrameworkCore;

namespace DiscordBot.Db;

public class SqliteApplicationContext : DbContext
{
    private readonly SqliteConfig _config;
    
    public DbSet<User> Users { get; set; }

    public SqliteApplicationContext(SqliteConfig config)
    {
        _config = config;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UsersModelConfigurator());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={_config.DataSource}");
    }
}