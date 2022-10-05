using Battleground.Models.Dtos;
using Battleground.Models.Exceptions;
using Battleground.Models.InputModels;
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
    public bool AddPokemonToPlayer(InventoryInputModel inventoryInput)
    {
        // Check if the player already has the pokemon
        var playerInventory = _dbContext.PlayerInventories.FirstOrDefault(x => x.PlayerId == inventoryInput.PlayerId && x.PokemonIdentifier == inventoryInput.PokemonIdentifier);
        
        if (playerInventory != null)
        {
            return false;
        }
        
        // Check if the pokemon exists in the on the web api
        var pokemon = _pokemonService.GetPokemonByName(inventoryInput.PokemonIdentifier);
        if(pokemon == null)
        {
            return false;
        }
        
        // Add the pokemon to the player
        var playerInventoryEntity = new PlayerInventory
        {
            PlayerId = inventoryInput.PlayerId,
            PokemonIdentifier = inventoryInput.PokemonIdentifier,
            AcquiredDate = DateTime.Now.ToUniversalTime()
        };
        
        try{
            _dbContext.PlayerInventories.Add(playerInventoryEntity);
            _dbContext.SaveChanges();
            return true;
        }catch
        {
            return false;
        }
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
