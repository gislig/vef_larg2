using Battleground.Repositories.Interfaces;

namespace Battleground.Repositories.Implementations;

public class BattlePokemonRepository : IBattlePokemonRepository
{
    private readonly BattlegroundDbContext _dbContext;
    public BattlePokemonRepository(BattlegroundDbContext dbContext)
    {
        _dbContext = dbContext;
    }
}