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

    public AttackService(BattlegroundDbContext dbContext, PokemonService pokemonService)
    {
        _dbContext = dbContext;
        _pokemonService = pokemonService;
    }

    public async Task<AttackDto> Attack(AttackInputModel attack)
    {
        var newAttackDto = new AttackDto();

        // Check if the attacker exists
        var attacker = _dbContext.Players.FirstOrDefault(b => b.Id == attack.AttackerId);
        if (attacker == null)
        {
            Console.WriteLine("Attacker does not exist");
            return newAttackDto;
        }
        
        // Check if the battle exists
        var battle = _dbContext.Battles.FirstOrDefault(b => b.Id == attack.BattleId);
        if (battle == null)
        {
            Console.WriteLine("Battle does not exist");
            return newAttackDto;
        }
        
        // Check if the player is in the battle
        var playerAttacks = _dbContext
            .BattlePlayers
            .Include(x => x.Player)
            .Include(x => x.Battle)
            .ThenInclude(x => x.BattleStatus)
            .Where(x => x.Battle.Id == attack.BattleId)
            .FirstOrDefault(x => x.Player.Id == attack.AttackerId);
        
        if (playerAttacks == null)
        {
            Console.WriteLine("Player does not exist in the battle");
            return newAttackDto;
        }

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
        
        // Check if the pokemonIdentifier exists on the owner
        var attackersPokemon = _dbContext
            .PlayerInventories
            .Where(x => x.Player.Id == attack.AttackerId)
            .FirstOrDefault(x => x.PokemonIdentifier == attack.PokemonIdentifier);

        // If the pokemon does not exist return null
        if (attackersPokemon == null)
        {
            Console.WriteLine("Attacker does not own this pokemon");
            return newAttackDto;
        }
        
        // Get the pokemons in battle
        var pokemonsInBattle = _dbContext
            .BattlePokemons
            .Include(x => x.Battle)
            .Where(x => x.Battle.Id == attack.BattleId)
            .ToList();

        Console.WriteLine(pokemonsInBattle.Count);
        if (pokemonsInBattle.Count != 2)
        {
            Console.WriteLine("There are not 2 pokemons in battle");
            return newAttackDto;
        }
        
        // Get the turns
        var turnsPokemon1 = _dbContext
            .Attacks
            .Where(x => x.BattlePokemon.BattleId == attack.BattleId)
            .Where(x => x.PokemonIdentifier == pokemonsInBattle[0].PokemonIdentifier)
            .ToList();

        var turnsPokemon2 = _dbContext
            .Attacks
            .Where(x => x.BattlePokemon.BattleId == attack.BattleId)
            .Where(x => x.PokemonIdentifier == pokemonsInBattle[1].PokemonIdentifier)
            .ToList();
        
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
        else
        {
            Console.WriteLine("Pokemon 2 attacks");
            pokemon2attacks = true;
        }
        
        // Get the pokemon status
        //var pokemon1 = _pokemonService.GetPokemonByName(pokemonsInBattle[0].PokemonIdentifier);
        //var pokemon2 = _pokemonService.GetPokemonByName(pokemonsInBattle[1].PokemonIdentifier);
        
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