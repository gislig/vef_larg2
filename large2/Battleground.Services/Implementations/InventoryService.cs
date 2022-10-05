using Battleground.Models.Dtos;
using Battleground.Models.Exceptions;
using Battleground.Repositories;
using Battleground.Repositories.Entities;
using Battleground.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Battleground.Services.Implementations;

public class InventoryService : IInventoryService
{
    private readonly BattlegroundDbContext _dbContext;
    private readonly IPokemonService _pokemonService;
    
    public InventoryService(BattlegroundDbContext dbContext, IPokemonService pokemonService)
    {
        _dbContext = dbContext;
        _pokemonService = pokemonService;
    }
    
    // Get all items in inventory
    public IEnumerable<PlayerInventory> GetInventoryItems()
    {
        var inventoryItems = _dbContext.PlayerInventories.ToList();
        
        return inventoryItems;
    }
    
    // Get all items in inventory by player id
    public IEnumerable<PlayerInventory> GetInventoryItemsByPlayerId(int playerId)
    {
        var inventoryItems = _dbContext.PlayerInventories.Where(x => x.PlayerId == playerId).ToList();
        
        return inventoryItems;
    }
    
    // Get all items in inventory by item id
    public IEnumerable<PlayerInventory> GetInventoryItemsByItemId(string pokemonIdentifier)
    {
        var inventoryItems = _dbContext.PlayerInventories.Where(x => x.PokemonIdentifier == pokemonIdentifier).ToList();
        
        return inventoryItems;
    }
    
    // Get all items in inventory by player id and item id
    //TODO FIx
    public PlayerInventory GetInventoryItemsByPlayerIdAndItemId(int playerId, string pokemonIdentifier)
    {
        // var inventoryItems = _dbContext.PlayerInventories.Where(x => x.PlayerId == playerId && x.PokemonIdentifier == pokemonIdentifier).ToList();
        
        // return inventoryItems;
        return null;
    }
    
    // Add pokemonIdentifier to playerID
    public bool AddPokemonToPlayer(int playerId, string pokemonIdentifier)
    {
        var player = _dbContext.Players.FirstOrDefaultAsync(x => x.Id == playerId);
        
        if (player == null)
        {
            // TODO: Create PlayerNotFoundException
            return false;
        }
        
        var pokemon = _pokemonService.GetPokemonByName(pokemonIdentifier);
        
        if (pokemon == null)
        {
            // TODO: Create PokemonNotFoundException
            return false;
        }
        
        var playerInventory = new PlayerInventory
        {
            PlayerId = playerId,
            PokemonIdentifier = pokemonIdentifier
        };
        
        _dbContext.PlayerInventories.Add(playerInventory);
        _dbContext.SaveChanges();
        return true;
    }
    
    // Remove pokemonIdentifier from playerID
    public bool RemovePokemonFromPlayer(int playerId, string pokemonIdentifier)
    {
        var playerInventory = _dbContext.PlayerInventories.FirstOrDefault(x => x.PlayerId == playerId && x.PokemonIdentifier == pokemonIdentifier);

        if (playerInventory == null)
        {
            return false;
            // TODO: Create PokemonNotFoundException
        }
        
        _dbContext.PlayerInventories.Remove(playerInventory);
        _dbContext.SaveChanges();
        return true;
    }
}
