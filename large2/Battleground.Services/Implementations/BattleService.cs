using Battleground.Models.Dtos;
using Battleground.Models.InputModels;
using Battleground.Services.Interfaces;
using Battleground.Repositories.Entities;
using Battleground.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Battleground.Services.Implementations;

public class BattleService : IBattleService
{
    private readonly BattlegroundDbContext _dbContext;
    public BattleService(BattlegroundDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    // (5%) battle - Should return a specific battle by id
    public async Task<Battle?> GetBattleById(int id)
    {
        var battle = await _dbContext.Battles
            .Include(b => b.Winner)
            .Include(b => b.BattlePlayers)
            .Include(b => b.BattlePokemons)
            .Include(b => b.BattleStatus)
            .FirstOrDefaultAsync(b => b.Id == id);

        return battle;        
    }
    
    //allBattles - Should return a collection of all battles. Contains a field
    //argument called status which is of type BattleStatus (enum) and should be
    //used to filter the data based on the status of the battle
    // TODO: Þarf að lagfæra þetta út frá þessum texta
    public async Task<IEnumerable<Battle?>> AllBattles()
    {
        var battle = await _dbContext.Battles
            .Include(b => b.Winner)
            .Include(b => b.BattlePlayers)
            .Include(b => b.BattlePokemons)
            .Include(b => b.BattleStatus)
            .ToListAsync();
        return battle;
    }
    
    // Create a new battle
    public async Task<BattleDto> CreateBattle(BattleInputModel battle)
    {
        var newBattleDto = new BattleDto();
        
        // If battle.Players count more than two then return null
        if (battle.Players.Count() > 2 || battle.Players.Count() < 2)
        {
            Console.WriteLine("Battle must have two players");
            return newBattleDto;
        }
        
        // If battle.Pokemons count more than two or less than two then return null
        if (battle.Pokemons.Count() > 2 || battle.Pokemons.Count() < 2)
        {
            Console.WriteLine("Battle must have two pokemons");
            return newBattleDto;
        }
        
        // get the first item in IEnumerable item of battle.Players
        var player1 = await _dbContext.Players.FindAsync(battle.Players.First());
        var player2 = await _dbContext.Players.FindAsync(battle.Players.Last());
        
        
        // If the player1 or player2 is not found then return null
        if (player1 == null || player2 == null)
        {
            Console.WriteLine("Player not found");
            return newBattleDto;
        }
        
        // Check if players are disabled if so then return null
        if (player1.Deleted == true || player2.Deleted == true)
        {
            Console.WriteLine("Player has been deleted");
            return newBattleDto;
        }
        
        
        // get the first and second item in IEnumerable item of battle.Pokemons 
        var rawPokemon1 = battle.Pokemons.First();
        var rawPokemon2 = battle.Pokemons.Last();

        if (rawPokemon1 == rawPokemon2)
        {
            Console.WriteLine("Pokemons must be different");
            return newBattleDto;
        }
        
        // Check if the player1 owns the pokemon1 and player2 owns the pokemon2 with the help of PlayerInventory
        var player1OwnsPokemon1 = _dbContext
            .PlayerInventories
            .Any(x => x.PlayerId == player1.Id && x.PokemonIdentifier == rawPokemon1);
        
        var player2OwnsPokemon2 = _dbContext
            .PlayerInventories
            .Any(x => x.PlayerId == player2.Id && x.PokemonIdentifier == rawPokemon2);
        

        var playersInBattle = await _dbContext
            .BattlePlayers
            .Include(x => x.Battle)
            .ThenInclude(x => x.BattleStatus)
            .Where(x => x.PlayerInMatchId == player2.Id || x.PlayerInMatchId == player1.Id)
            .Where(x => x.Battle.BattleStatus.Name == "NOT_STARTED" 
                        || x.Battle.BattleStatus.Name == "STARTED")
            .ToListAsync();

        if(playersInBattle.Count() >= 1)
        {
            Console.WriteLine("Player is already in a battle");
            return newBattleDto;
        }
        
        // If the player1 does not own the pokemon1 or player2 does not own the pokemon2 then return null
        if (!player1OwnsPokemon1 || !player2OwnsPokemon2)
        {
            Console.WriteLine("Player does not own the pokemon");
            return newBattleDto;
        }
        // Create new battlestatus
        BattleStatus? newBattleStatus = new BattleStatus()
        {
            Name = "NOT_STARTED"
        };
        
        // Insert battleStatus to database
        _dbContext.BattleStatuses.Add(newBattleStatus);
        await _dbContext.SaveChangesAsync();
        // Get id of the newly created battleStatus
        var battleStatusId = newBattleStatus.Id;
        Console.WriteLine("The id of the battle is : " + battleStatusId);
        // Create new Battle
        Battle newBattle = new Battle
        {
            // Player1 is set temporarily while the battle is ongoing
            Winner = player1,
            StatusId = battleStatusId
        };

        try
        {
            // Insert battle to database
            _dbContext.Battles.Add(newBattle);
            await _dbContext.SaveChangesAsync();
        }
        catch
        {
            Console.WriteLine("Could not create battle");
            return null;
        }

        // Get id of the battle
        var battleId = newBattle.Id;
        
        BattlePlayer battlePlayer1 = new BattlePlayer
        {
            PlayerInMatchId = player1.Id,
            BattlesId = battleId,
        };

        BattlePlayer battlePlayer2 = new BattlePlayer
        {
            PlayerInMatchId = player2.Id,
            BattlesId = battleId,
        };
        
        BattlePokemon battlePokemon1 = new BattlePokemon
        {
            PokemonIdentifier = rawPokemon1,
            BattleId = battleId,
        };
        
        BattlePokemon battlePokemon2 = new BattlePokemon
        {
            PokemonIdentifier = rawPokemon2,
            BattleId = battleId,
        };        
        
        // Insert battlePlayer1 and battlePlayer2 to database
        _dbContext.BattlePlayers.Add(battlePlayer1);
        _dbContext.BattlePlayers.Add(battlePlayer2);
        // Insert battlePokemon1 and battlePokemon2 to database
        _dbContext.BattlePokemons.Add(battlePokemon1);
        _dbContext.BattlePokemons.Add(battlePokemon2);
        
        try{
            // Save changes to database
            await _dbContext.SaveChangesAsync();
            newBattleDto.Id = newBattle.Id;
            newBattleDto.StatusId = newBattleStatus.Id;
            
            return newBattleDto;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return newBattleDto;
        }
    }
}