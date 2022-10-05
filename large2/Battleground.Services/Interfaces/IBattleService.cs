
using Battleground.Services.Interfaces;
using Battleground.Repositories.Entities;
using Battleground.Repositories;
using Microsoft.EntityFrameworkCore;
namespace Battleground.Services.Interfaces;

public interface IBattleService
{
    Battle CreateBattle(Battle battle);
    Battle UpdateBattle(Battle battle);
    Battle DeleteBattle(Battle battle);
    IEnumerable<Attack> GetAllAttacks();
    Attack? GetAttackById(int id);

}