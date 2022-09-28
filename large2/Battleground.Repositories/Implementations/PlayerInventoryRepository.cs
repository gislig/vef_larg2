using Battleground.Repositories.Interfaces;

namespace Battleground.Repositories.Implementations;

public class PlayerInventoryRepository : IPlayerInventoryRepository
{
    private readonly BattlegroundDbContext _dbContext;
    public PlayerInventoryRepository(BattlegroundDbContext dbContext)
    {
        _dbContext = dbContext;
    }
}