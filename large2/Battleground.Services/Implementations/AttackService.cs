using Battleground.Models.Dtos;
using Battleground.Models.InputModels;
using Battleground.Repositories;
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
            //_dbContext.SaveChanges();
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
        var pokemon1attacks = false;
        var pokemon2attacks = false;
        
        // If turn is even, pokemon 1 attacks else pokemon 2 attacks
        if (attackPokemon1Turn == attackPokemon2Turn)
        {
            Console.WriteLine("Pokemon 1 attacks");
            pokemon1attacks = true;
        }
        else if (attackPokemon1Turn > attackPokemon2Turn)
        {
            Console.WriteLine("Pokemon 2 attacks");
            pokemon2attacks = true;
        }else if (attackPokemon2Turn > attackPokemon1Turn)
        {
            Console.WriteLine("Pokemon 1 attacks");
            pokemon1attacks = true;
        }

        // Get the pokemon status
        var pokemon1 = await _pokemonService.GetPokemonByName(pokemonsInBattle[0].PokemonIdentifier);
        var pokemon2 = await _pokemonService.GetPokemonByName(pokemonsInBattle[1].PokemonIdentifier);
        //var pokemon1 = _pokemonService.GetPokemonByName(pokemonsInBattle[0].PokemonIdentifier);
        //var pokemon2 = _pokemonService.GetPokemonByName(pokemonsInBattle[1].PokemonIdentifier);
        Console.WriteLine("Fighter number 1 is : " + pokemon1.name);
        Console.WriteLine("Fighter number 2 is : " + pokemon2.name);
        
        
        // Each attack has a 30% chance of being a critical attack and
        // if the attack is a critical attack it will become 40% more
        // powerful than the base attack
        
        
        // Each attack has a 15% chance of not hitting the opponent
        // - if that is the case no damage is made to the opponent during that turn
        
        
        Console.WriteLine("I attacked");
        // Check how many turns have passed since the last attack and see if it is the attackers turn
        // If it is not the attackers turn, return an error message
        // If it is the attackers turn, continue with the attack
        return new AttackDto();
    } 
}