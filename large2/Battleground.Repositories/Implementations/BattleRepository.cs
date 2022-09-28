using Battleground.Repositories.Entities;
using Battleground.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Battleground.Repositories.Implementations;

public class BattleRepository : IBattleRepository
{
    private readonly BattlegroundDbContext _dbContext;
    
    public BattleRepository(BattlegroundDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    // (5%) battle - Should return a specific battle by id
    public async Task<Battle?> GetBattleById(int id)
    {
        return await _dbContext.Battles.FindAsync(id);
    }
    
    //allBattles - Should return a collection of all battles. Contains a field
    //argument called status which is of type BattleStatus (enum) and should be
    //used to filter the data based on the status of the battle
    // TODO: Þarf að lagfæra þetta út frá þessum texta
    public async Task<IEnumerable<Battle?>> AllBattles()
    {
        return await _dbContext.Battles.ToListAsync();
    }
    
    // Create a new battle
    public async Task<Battle> CreateBattle(Battle battle)
    {
        _dbContext.Battles.Add(battle);
        await _dbContext.SaveChangesAsync();
        return battle;
    }
    
    // Update a battle
    public async Task<Battle> UpdateBattle(Battle battle)
    {
        _dbContext.Battles.Update(battle);
        await _dbContext.SaveChangesAsync();
        return battle;
    }
    
    // Delete a battle
    public async Task<Battle> DeleteBattle(Battle battle)
    {
        _dbContext.Battles.Remove(battle);
        await _dbContext.SaveChangesAsync();
        return battle;
    }
}