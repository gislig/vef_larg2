using Battleground.Repositories.Interfaces;

namespace Battleground.Repositories.Implementations;

public class BattlePlayerRepository : IBattlePlayerRepository
{
    private readonly BattlegroundDbContext _dbContext;
    public BattlePlayerRepository(BattlegroundDbContext dbContext)
    {
        _dbContext = dbContext;
    }
}