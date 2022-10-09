using Battleground.Models.Dtos;
using Battleground.Models.InputModels;

namespace Battleground.Services.Interfaces;

public interface IAttackService
{
    Task<AttackDto> Attack(AttackInputModel attack);
}