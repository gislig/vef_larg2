using Battleground.Models.Dtos;
using Battleground.Models.InputModels;

namespace Battleground.Services.Interfaces;

public interface IAttackService
{
    AttackDto Attack(AttackInputModel attack);
}