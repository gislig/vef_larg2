using Battleground.Models.Dtos;
using Battleground.Models.InputModels;
using Battleground.Repositories;
using Battleground.Services.Interfaces;

namespace Battleground.Services.Implementations;

public class AttackService : IAttackService
{
    private readonly BattlegroundDbContext _dbContext;
    public AttackService(BattlegroundDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public AttackDto Attack(AttackInputModel attack)
    {
        return new AttackDto();
    } 
}