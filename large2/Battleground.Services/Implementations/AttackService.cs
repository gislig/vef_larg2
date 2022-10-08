using Battleground.Models.Dtos;
using Battleground.Models.InputModels;
using Battleground.Repositories;
using Battleground.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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
        var newAttackDto = new AttackDto();

        // Check if the attacker exists
        var attacker = _dbContext.Players.FirstOrDefault(b => b.Id == attack.AttackerId);
        if (attacker == null)
        {
            throw new Exception("Attacker does not exist");
        }
        
        // Check if the battle exists, it is not finished and the attacker is part of it
        var battlePlayer = _dbContext
            .BattlePlayers
            .Include(x => x.Player)
            .Where(x => x.Player.Id == attack.AttackerId)
            .Include(x => x.Battle)
            .ThenInclude(x => x.BattleStatus)
            .Where(x => x.Battle.Id == attack.BattleId)
            .FirstOrDefault(x =>
                x.Battle.BattleStatus != null && (x.Battle.BattleStatus.Name == "NOT_STARTED" || x.Battle.BattleStatus.Name == "IN_PROGRESS"));
        
        if (battlePlayer == null)
        {
            Console.WriteLine("Could not find battle player");
        }

        Console.WriteLine("Found battle player");
        
        // Check if battle is NOT_STARTED and if so, start it
        if (battlePlayer?.Battle.BattleStatus?.Name == "NOT_STARTED")
        {
            Console.WriteLine("Starting battle");
            battlePlayer.Battle.BattleStatus = _dbContext.BattleStatuses.FirstOrDefault(x => x != null && x.Name == "IN_PROGRESS");
            _dbContext.SaveChanges();
        }
        
        // Check how many turns have passed since the last attack and see if it is the attackers turn
        // If it is not the attackers turn, return an error message
        // If it is the attackers turn, continue with the attack
        
        


        return new AttackDto();
    } 
}