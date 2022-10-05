using Battleground.Services.Interfaces;
using Battleground.Repositories.Entities;
using Battleground.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Battleground.Services.Implementations;

public class BattleService : IBattleService
{
    private readonly BattlegroundDbContext _dbContext;
    public BattleService(BattlegroundDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    // (5%) battle - Should return a specific battle by id
    public Battle? GetBattleById(int id)
    {
        return _dbContext.Battles.Find(id);
    }
    
    //allBattles - Should return a collection of all battles. Contains a field
    //argument called status which is of type BattleStatus (enum) and should be
    //used to filter the data based on the status of the battle
    // TODO: Þarf að lagfæra þetta út frá þessum texta
    public IEnumerable<Battle?> AllBattles()
    {
        return _dbContext.Battles.ToList();
    }
    
    // Create a new battle
    public Battle CreateBattle(Battle battle)
    {
        _dbContext.Battles.Add(battle);
        _dbContext.SaveChanges();
        return battle;
    }
    
    // Update a battle
    public Battle UpdateBattle(Battle battle)
    {
        _dbContext.Battles.Update(battle);
        _dbContext.SaveChanges();
        return battle;
    }
    
    // Delete a battle
    public Battle DeleteBattle(Battle battle)
    {
        _dbContext.Battles.Remove(battle);
        _dbContext.SaveChanges();
        return battle;
    }
    public IEnumerable<Attack> GetAllAttacks()
    {
        return _dbContext.Attacks;
    }
    
    // Get a specific attack from the database async
    public Attack? GetAttackById(int id)
    {
        return _dbContext.Attacks.Find(id);
    }
}