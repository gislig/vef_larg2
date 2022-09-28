using Battleground.Repositories.Entities;

namespace Battleground.Repositories.Interfaces;

public interface IBattleRepository
{
    Task<IEnumerable<Battle?>> AllBattles();
    Task<Battle?> GetBattleById(int id);
    Task<Battle> CreateBattle(Battle battle);
    Task<Battle> UpdateBattle(Battle battle);
    Task<Battle> DeleteBattle(Battle battle);
}