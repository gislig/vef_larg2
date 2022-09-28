using Battleground.Repositories.Entities;
using Battleground.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Battleground.Repositories.Implementations;

public class PlayerRepository : IPlayerRepository
{
    private readonly BattlegroundDbContext _dbContext;
    public PlayerRepository(BattlegroundDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    // (5%) player - Should return a specific player by id
    public async Task<Player?> GetPlayerById(int id)
    {
        return await _dbContext.Players.FindAsync(id);
    }
    
    // (5%) allPlayers - Should return a collection of all players
    public async Task<IEnumerable<Player?>> AllPlayers()
    {
        return await _dbContext.Players.ToListAsync();
    }
    
    // Create a new player
    public async Task<Player?> CreatePlayer(Player player)
    {
        _dbContext.Players.Add(player);
        await _dbContext.SaveChangesAsync();
        return player;
    }
    
    // Update Player
    public async Task<Player?> UpdatePlayer(Player player)
    {
        _dbContext.Players.Update(player);
        await _dbContext.SaveChangesAsync();
        return player;
    }
    
    // Delete Player
    public async Task<Player?> DeletePlayer(int id)
    {
        var player = await _dbContext.Players.FindAsync(id);
        _dbContext.Players.Remove(player);
        await _dbContext.SaveChangesAsync();
        return player;
    }
}