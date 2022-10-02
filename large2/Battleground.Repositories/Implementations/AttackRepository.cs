using Battleground.Repositories.Entities;
using Battleground.Repositories.Interfaces;

namespace Battleground.Repositories.Implementations;

public class AttackRepository : IAttackRepository
{
    private readonly BattlegroundDbContext _dbContext;
    public AttackRepository(BattlegroundDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    // Get all attacks from the database
    public IEnumerable<Attack> GetAllAttacks()
    {
        return _dbContext.Attacks;
    }
    
    // Get a specific attack from the database async
    public async Task<Attack?> GetAttackAsync(int id)
    {
        return await _dbContext.Attacks.FindAsync(id);
    }

    
}