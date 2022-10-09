using Battleground.Models.Dtos;
using Battleground.Models.InputModels;
using Battleground.Repositories;
using Battleground.Repositories.Entities;
using Battleground.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Battleground.Services.Implementations;

public class AttackService : IAttackService
{
    private readonly BattlegroundDbContext _dbContext;
    private readonly IPokemonService _pokemonService;

    public AttackService(BattlegroundDbContext dbContext, IPokemonService pokemonService)
    {
        _dbContext = dbContext;
        _pokemonService = pokemonService;
    }

    private int ChanceToHit(int percentage)
    {
        Random r = new Random();
        return r.Next(0, percentage);
    }
    
    private int CalculateDamage(int baseAttack, int attackHitChance, int criticalChance)
    {
        if (attackHitChance == 0)
        {
            return 0;
        }
        
        if(criticalChance == 40)
        {
            return (int) (baseAttack * 1.4);
        }
        else if (criticalChance > 30)
        {
            return (int) (baseAttack * 1.3);
        }
        else
        {
            return (int) (baseAttack);
        }
    }
    
    public async Task<AttackDto> Attack(AttackInputModel attack)
    {
        var newAttackDto = new AttackDto();

        Console.WriteLine("Checking if attacker exists...");
        Console.WriteLine(attack.AttackerId + " " + attack.BattleId + " " + attack.PokemonIdentifier);
        // Check if the attacker exists
        var attacker = await _dbContext.Players.FirstOrDefaultAsync(b => b.Id == attack.AttackerId);
        if (attacker == null)
        {
            Console.WriteLine("Attacker does not exist");
            return newAttackDto;
        }
        
        Console.WriteLine("Checking if battle exists...");
        // Check if the battle exists
        var battle = await _dbContext.Battles.FirstOrDefaultAsync(b => b.Id == attack.BattleId);
        if (battle == null)
        {
            Console.WriteLine("Battle does not exist");
            return newAttackDto;
        }
        
        Console.WriteLine("Checking if player is in the battle...");
        // Check if the player is in the battle
        var playerAttacks = await _dbContext
            .BattlePlayers
            .Include(x => x.Player)
            .Include(x => x.Battle)
            .ThenInclude(x => x.BattleStatus)
            .Where(x => x.Battle.Id == attack.BattleId)
            .FirstOrDefaultAsync(x => x.Player.Id == attack.AttackerId);
        
        if (playerAttacks == null)
        {
            Console.WriteLine("Player does not exist in the battle");
            return newAttackDto;
        }

        Console.WriteLine("Checking if battle is active...");
        // Check if the battle is not finished
        if (playerAttacks.Battle.BattleStatus.Name == "FINISHED")
        {
            Console.WriteLine("Battle is finished");
            return newAttackDto;
        }
        
        // If the battle is not started, start it
        if (playerAttacks.Battle.BattleStatus.Name == "NOT_STARTED")
        {
            playerAttacks.Battle.BattleStatus.Name = "STARTED";
            Console.WriteLine("The battle has started");
            _dbContext.SaveChanges();
        }
        Console.WriteLine("Checking if pokemonIdentifier exists...");
        // Check if the pokemonIdentifier exists on the owner
        var attackersPokemon = await _dbContext
            .PlayerInventories
            .Where(x => x.Player.Id == attack.AttackerId)
            .FirstOrDefaultAsync(x => x.PokemonIdentifier == attack.PokemonIdentifier);

        // If the pokemon does not exist return null
        if (attackersPokemon == null)
        {
            Console.WriteLine("Attacker does not own this pokemon");
            return newAttackDto;
        }
        
        // Get the pokemons in battle
        var pokemonsInBattle = await _dbContext
            .BattlePokemons
            .Include(x => x.Battle)
            .Where(x => x.Battle.Id == attack.BattleId)
            .ToListAsync();

        Console.WriteLine(pokemonsInBattle.Count);
        if (pokemonsInBattle.Count != 2)
        {
            Console.WriteLine("There are not 2 pokemons in battle");
            return newAttackDto;
        }
        
        // Get the turns
        var turnsPokemon1 = await _dbContext
            .Attacks
            .Where(x => x.BattlePokemon.BattleId == attack.BattleId)
            .Where(x => x.PokemonIdentifier == pokemonsInBattle[0].PokemonIdentifier)
            .ToListAsync();

        var turnsPokemon2 = await _dbContext
            .Attacks
            .Where(x => x.BattlePokemon.BattleId == attack.BattleId)
            .Where(x => x.PokemonIdentifier == pokemonsInBattle[1].PokemonIdentifier)
            .ToListAsync();
        
        // Get count of the turns
        var attackPokemon1Turn = turnsPokemon1.Count;
        var attackPokemon2Turn = turnsPokemon2.Count;
        Console.WriteLine(attackPokemon1Turn + " " + attackPokemon2Turn);
        var pokemon1attacks = false;
        var pokemon2attacks = false;
        
        // If turn is even, pokemon 1 attacks else pokemon 2 attacks
        if (attackPokemon1Turn == attackPokemon2Turn)
        {
            Console.WriteLine("Pokemon 1 should be attacking");
            pokemon1attacks = true;
        }
        else if (attackPokemon1Turn < attackPokemon2Turn)
        {
            Console.WriteLine("Pokemon 2 should be attacking");
            pokemon2attacks = true;
        }else if (attackPokemon2Turn < attackPokemon1Turn)
        {
            Console.WriteLine("Pokemon 1 should be attacking");
            pokemon1attacks = true;
        }

        // If pokemon1 is attacking and it should be pokemon2s turn, return null
        if (pokemon1attacks && pokemonsInBattle[0].PokemonIdentifier != attack.PokemonIdentifier)
        {
            Console.WriteLine("Pokemon 1 is attacking but it is not its turn");
            return newAttackDto;
        }
        
        // If pokemon2 is attacking and it should be pokemon1s turn, return null
        if (pokemon2attacks && pokemonsInBattle[1].PokemonIdentifier != attack.PokemonIdentifier)
        {
            Console.WriteLine("Pokemon 2 is attacking but it is not its turn");
            return newAttackDto;
        }
        
        // Get the pokemon status
        var pokemon1 = await _pokemonService.GetPokemonByName(pokemonsInBattle[0].PokemonIdentifier);
        var pokemon2 = await _pokemonService.GetPokemonByName(pokemonsInBattle[1].PokemonIdentifier);
        //var pokemon1 = _pokemonService.GetPokemonByName(pokemonsInBattle[0].PokemonIdentifier);
        //var pokemon2 = _pokemonService.GetPokemonByName(pokemonsInBattle[1].PokemonIdentifier);
        Console.WriteLine("Fighter number 1 is : " + pokemon1.name + " and is at " + pokemon1.healthPoints + " hp" + ". It is his turn :" + pokemon1attacks);
        Console.WriteLine("Fighter number 2 is : " + pokemon2.name + " and is at " + pokemon2.healthPoints + " hp" + ". It is his turn :" + pokemon2attacks);
        
        // Get current health of the pokemon 1
        var pokemon1Health = pokemon1.healthPoints;
        // Get current health from attacks from database
        var pokemon1HealthFromAttacks = await _dbContext
            .Attacks
            .Where(x => x.BattlePokemon.BattleId == attack.BattleId)
            .Where(x => x.PokemonIdentifier == pokemonsInBattle[0].PokemonIdentifier)
            .SumAsync(x => x.Damage);
        
        Console.WriteLine("Pokemon 1 has " + pokemon1HealthFromAttacks + " damage");
        pokemon1Health -= pokemon1HealthFromAttacks;
        Console.WriteLine("Total health of pokemon 1 is " + pokemon1Health);
        
        // Get current health of the pokemon 2
        var pokemon2Health = pokemon2.healthPoints;
        // Get current health from attacks from database
        var pokemon2HealthFromAttacks = await _dbContext
            .Attacks
            .Where(x => x.BattlePokemon.BattleId == attack.BattleId)
            .Where(x => x.PokemonIdentifier == pokemonsInBattle[1].PokemonIdentifier)
            .SumAsync(x => x.Damage);
        
        Console.WriteLine("Pokemon 2 has " + pokemon2HealthFromAttacks + " damage");
        pokemon2Health -= pokemon2HealthFromAttacks;
        Console.WriteLine("Total health of pokemon 2 is " + pokemon2Health);
        
        // Each attack has a 30% chance of being a critical attack and
        // if the attack is a critical attack it will become 40% more
        // powerful than the base attack
        var criticalAttack = ChanceToHit(40);
        
        // Each attack has a 15% chance of not hitting the opponent
        // - if that is the case no damage is made to the opponent during that turn
        var attackHitChance = ChanceToHit(15);
        bool attackSuccess = attackHitChance != 0;
        
        /*
        pokemon1.weight = pokemon1.weight / 10;
        pokemon1.baseAttack = pokemon1.baseAttack / 10;
        pokemon1.healthPoints = pokemon1.healthPoints / 10;
        pokemon1.name;
        pokemon1.owners;
        */

        // Calculate the damage with baseAttack and weight with attackHitChance and criticalAttack
        var damagePokemon1 = CalculateDamage(pokemon1.baseAttack, attackHitChance, criticalAttack);
        var damagePokemon2 = CalculateDamage(pokemon2.baseAttack, attackHitChance, criticalAttack);
        
        Console.WriteLine("pokemon1 damage is : " + damagePokemon1);
        Console.WriteLine("pokemon2 damage is : " + damagePokemon2);
        
        pokemon1Health -= damagePokemon2;
        pokemon2Health -= damagePokemon1;

        // Save the attack to the database for pokemon1
        var newAttackPokemon1 = new Attack
        {
            Success = attackSuccess,
            CriticalHit = criticalAttack,
            Damage = damagePokemon1,
            PokemonIdentifier = pokemonsInBattle[0].PokemonIdentifier,
            BattlePokemonId = pokemonsInBattle[0].Id,
            BattlePokemon = pokemonsInBattle[0]
        };
        
        // Save the attack to the database for pokemon2
        var newAttackPokemon2 = new Attack
        {
            Success = attackSuccess,
            CriticalHit = criticalAttack,
            Damage = damagePokemon1,
            PokemonIdentifier = pokemonsInBattle[1].PokemonIdentifier,
            BattlePokemonId = pokemonsInBattle[1].Id,
            BattlePokemon = pokemonsInBattle[1]
        };

        if(pokemon1attacks)
        {
            Console.WriteLine("Pokemon 1 attacks");
            if (pokemon1Health <= 0)
            {
                Console.WriteLine("Game is finished, set player 2 as winner");
                // save the winner to the database
                playerAttacks.Battle.WinnerId = attacker.Id;
                playerAttacks.Battle.BattleStatus.Name = "FINISHED";
                // save the battle
                await _dbContext.SaveChangesAsync();
            }
            
            _dbContext.Attacks.Add(newAttackPokemon2);
            await _dbContext.SaveChangesAsync();
            newAttackDto.Damage = newAttackPokemon2.Damage;
            newAttackDto.Success = newAttackPokemon2.Success;
            newAttackDto.CriticalHit = newAttackPokemon2.CriticalHit;
            newAttackDto.PokemonIdentifier = newAttackPokemon2.PokemonIdentifier;
            newAttackDto.BattlePokemonId = newAttackPokemon2.BattlePokemonId;
            newAttackDto.BattleId = newAttackPokemon2.BattlePokemon.BattleId;
            
            
        }
        else if(pokemon2attacks)
        {
            if (pokemon2Health <= 0)
            {
                Console.WriteLine("Game is finished, set player 1 as winner");
                // save the winner to the database
                playerAttacks.Battle.WinnerId = attacker.Id;
                playerAttacks.Battle.BattleStatus.Name = "FINISHED";
                // save the battle
                await _dbContext.SaveChangesAsync();                
            }
            Console.WriteLine("Pokemon 2 attacks");
            _dbContext.Attacks.Add(newAttackPokemon1);
            await _dbContext.SaveChangesAsync();
            newAttackDto.Damage = newAttackPokemon1.Damage;
            newAttackDto.Success = newAttackPokemon1.Success;
            newAttackDto.CriticalHit = newAttackPokemon1.CriticalHit;
            newAttackDto.PokemonIdentifier = newAttackPokemon1.PokemonIdentifier;
            newAttackDto.BattlePokemonId = newAttackPokemon1.BattlePokemonId;
            newAttackDto.BattleId = newAttackPokemon1.BattlePokemon.BattleId;
        }
        
        Console.WriteLine("I attacked");
        // Check how many turns have passed since the last attack and see if it is the attackers turn
        // If it is not the attackers turn, return an error message
        // If it is the attackers turn, continue with the attack
        return newAttackDto;
    } 
}