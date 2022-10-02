
using Battleground.Services.Interfaces;
using Battleground.Repositories.Entities;
using Battleground.Repositories;
using Microsoft.EntityFrameworkCore;
namespace Battleground.Services.Interfaces;

public interface IBattleService
{
    public Task<Battle> CreateBattle(Battle battle);
    public Task<Battle> UpdateBattle(Battle battle);
    public Task<Battle> DeleteBattle(Battle battle);
    public IEnumerable<Attack> GetAllAttacks();
    public Task<Attack?> GetAttackAsync(int id);

}