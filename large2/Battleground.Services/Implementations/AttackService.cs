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
            Console.WriteLine("Attacker does not exist");
        }

        // Check if the battle exists
        var playerAttacks = _dbContext
            .BattlePlayers
            .Include(x => x.Player)
            .Include(x => x.Battle)
            .ThenInclude(x => x.BattleStatus)
            .FirstOrDefault(x => x.Player.Id == attack.AttackerId);
        
        if (playerAttacks == null)
        {
            Console.WriteLine("Battle does not exist");
            return null;
        }
        Console.WriteLine("The battle exists");

        // Check if the battle is not finished
        if (playerAttacks.Battle.BattleStatus.Name == "FINISHED")
        {
            Console.WriteLine("Battle is finished");
            return null;
        }
        
        // If the battle is not started, start it
        if (playerAttacks.Battle.BattleStatus.Name == "NOT_STARTED")
        {
            playerAttacks.Battle.BattleStatus.Name = "STARTED";
            Console.WriteLine("The battle has started");
            //_dbContext.SaveChanges();
        }
        
        // Get the pokemons in battle

        
        // Check how many turns have passed since the last attack and see if it is the attackers turn
        // If it is not the attackers turn, return an error message
        // If it is the attackers turn, continue with the attack
        
        


        return new AttackDto();
    } 
}