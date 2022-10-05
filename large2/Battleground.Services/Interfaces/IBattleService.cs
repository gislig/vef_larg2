
using Battleground.Services.Interfaces;
using Battleground.Repositories.Entities;
using Battleground.Repositories;
using Microsoft.EntityFrameworkCore;
namespace Battleground.Services.Interfaces;

public interface IBattleService
{
    Task<Battle> CreateBattle(Battle battle);
    Task<Battle> UpdateBattle(Battle battle);
    Task<Battle> DeleteBattle(Battle battle);
    Task<IEnumerable<Attack>> GetAllAttacks();
    Task<IEnumerable<Battle>> AllBattles();
    Task<Attack?> GetAttackAsync(int id);

}