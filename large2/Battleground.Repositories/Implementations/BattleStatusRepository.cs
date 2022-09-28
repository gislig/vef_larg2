using Battleground.Repositories.Interfaces;

namespace Battleground.Repositories.Implementations;

public class BattleStatusRepository : IBattleStatusRepository
{
    private readonly BattlegroundDbContext _dbContext;
    public BattleStatusRepository(BattlegroundDbContext dbContext)
    {
        _dbContext = dbContext;
    }
}