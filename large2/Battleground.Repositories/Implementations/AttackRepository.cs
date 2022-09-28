using Battleground.Repositories.Interfaces;

namespace Battleground.Repositories.Implementations;

public class AttackRepository : IAttackRepository
{
    private readonly BattlegroundDbContext _dbContext;
    public AttackRepository(BattlegroundDbContext dbContext)
    {
        _dbContext = dbContext;
    }
}