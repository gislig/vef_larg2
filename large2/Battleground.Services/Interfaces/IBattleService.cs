using Battleground.Models.Dtos;
using Battleground.Models.InputModels;
using Battleground.Services.Interfaces;
using Battleground.Repositories.Entities;
using Battleground.Repositories;
using Microsoft.EntityFrameworkCore;
namespace Battleground.Services.Interfaces;

public interface IBattleService
{
    BattleDto CreateBattle(BattleInputModel battle);
    BattleDto UpdateBattle(Battle battle);
    Battle DeleteBattle(Battle battle);
    Battle? GetBattleById(int id);
    IEnumerable<Attack> GetAllAttacks();
    Attack? GetAttackById(int id);
}