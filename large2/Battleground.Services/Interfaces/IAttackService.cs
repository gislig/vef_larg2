using Battleground.Models.Dtos;
using Battleground.Models.InputModels;
using Battleground.Repositories.Entities;

namespace Battleground.Services.Interfaces;

public interface IAttackService
{
    Task<AttackDto> Attack(AttackInputModel attack);

    Task<IEnumerable<Attack>> GetAttacksByBattlePokemons(int id);
}