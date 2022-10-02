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
    public async Task<List<PlayerInventory>> GetInventoryItems()
    {
        var inventoryItems = await _dbContext.PlayerInventories.ToListAsync();
        
        return inventoryItems;
    }
    
    // Get all items in inventory by player id
    public async Task<List<PlayerInventory>> GetInventoryItemsByPlayerId(int playerId)
    {
        var inventoryItems = await _dbContext.PlayerInventories.Where(x => x.PlayerId == playerId).ToListAsync();
        
        return inventoryItems;
    }
    
    // Get all items in inventory by item id
    public async Task<List<PlayerInventory>> GetInventoryItemsByItemId(string pokemonIdentifier)
    {
        var inventoryItems = await _dbContext.PlayerInventories.Where(x => x.PokemonIdentifier == pokemonIdentifier).ToListAsync();
        
        return inventoryItems;
    }
    
    // Get all items in inventory by player id and item id
    public async Task<List<PlayerInventory>> GetInventoryItemsByPlayerIdAndItemId(int playerId, string pokemonIdentifier)
    {
        var inventoryItems = await _dbContext.PlayerInventories.Where(x => x.PlayerId == playerId && x.PokemonIdentifier == pokemonIdentifier).ToListAsync();
        
        return inventoryItems;
    }
    
    // Add pokemonIdentifier to playerID
    public async Task AddPokemonToPlayer(int playerId, string pokemonIdentifier)
    {
        var player = await _dbContext.Players.FirstOrDefaultAsync(x => x.Id == playerId);
        
        if (player == null)
        {
            // TODO: Create PlayerNotFoundException
            return;
        }
        
        var pokemon = await _pokemonService.GetPokemonByName(pokemonIdentifier);
        
        if (pokemon == null)
        {
            // TODO: Create PokemonNotFoundException
            return;
        }
        
        var playerInventory = new PlayerInventory
        {
            PlayerId = playerId,
            PokemonIdentifier = pokemonIdentifier
        };
        
        await _dbContext.PlayerInventories.AddAsync(playerInventory);
        await _dbContext.SaveChangesAsync();
    }
    
    // Remove pokemonIdentifier from playerID
    public async Task RemovePokemonFromPlayer(int playerId, string pokemonIdentifier)
    {
        var playerInventory = await _dbContext.PlayerInventories.FirstOrDefaultAsync(x => x.PlayerId == playerId && x.PokemonIdentifier == pokemonIdentifier);

        if (playerInventory == null)
        {
            // TODO: Create PokemonNotFoundException
        }
        
        _dbContext.PlayerInventories.Remove(playerInventory);
        await _dbContext.SaveChangesAsync();
    }
}
