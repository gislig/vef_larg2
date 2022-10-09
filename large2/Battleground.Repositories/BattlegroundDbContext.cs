using Battleground.Repositories.Entities;
using Microsoft.EntityFrameworkCore;

namespace Battleground.Repositories
{
    /*
     dotnet ef migrations add InitialCreate
     dotnet ef database update 
     */
    public class BattlegroundDbContext : DbContext
    {
        
        public BattlegroundDbContext(DbContextOptions<BattlegroundDbContext> options) : base(options) {}
        
        public DbSet<Attack> Attacks { get; set; }
        public DbSet<Battle?> Battles { get; set; }
        public DbSet<BattlePlayer> BattlePlayers { get; set; }
        public DbSet<BattlePokemon> BattlePokemons { get; set; }
        public DbSet<BattleStatus?> BattleStatuses { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerInventory> PlayerInventories { get; set; }
        
    }
}