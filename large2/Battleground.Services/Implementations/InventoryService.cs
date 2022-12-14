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
    public async Task<IEnumerable<PlayerInventory>> GetInventoryItems()
    {
        var inventoryItems = await _dbContext.PlayerInventories
        .Include(x => x.Player).ToListAsync();
        
        return inventoryItems;
    }
    
    // Get all items in inventory by player id
    public async Task<IEnumerable<PlayerInventory>> GetInventoryItemsByPlayerId(int playerId)
    {
        var inventoryItems = await _dbContext.PlayerInventories.Where(x => x.PlayerId == playerId).ToListAsync();
        
        return inventoryItems;
    }
    
    // Get all items in inventory by item id
    public async Task<IEnumerable<PlayerInventory>> GetInventoryItemsByItemId(string pokemonIdentifier)
    {
        var inventoryItems = await _dbContext.PlayerInventories
            .Include(x => x.Player)
            .Where(x => x.PokemonIdentifier == pokemonIdentifier).ToListAsync();
        
        return inventoryItems;
    }
    
    // Get all items in inventory by player id and item id
    //TODO FIx
    public async Task<PlayerInventory> GetInventoryItemsByPlayerIdAndItemId(int playerId, string pokemonIdentifier)
    {
        // var inventoryItems = _dbContext.PlayerInventories.Where(x => x.PlayerId == playerId && x.PokemonIdentifier == pokemonIdentifier).ToList();
        
        // return inventoryItems;
        return null;
    }
    
    // Add pokemonIdentifier to playerID
    public async Task<bool> AddPokemonToPlayer(InventoryInputModel inventoryInput)
    {
        // Check if the player already has the pokemon
        var playerInventory = await _dbContext.PlayerInventories.FirstOrDefaultAsync(x => x.PlayerId == inventoryInput.PlayerId && x.PokemonIdentifier.ToLower() == inventoryInput.PokemonIdentifier.ToLower());
        
        if (playerInventory != null)
        {
            return false;
        }
        
        // Check if the pokemon exists in the on the web api
        var pokemon = await _pokemonService.GetPokemonByName(inventoryInput.PokemonIdentifier);
        if(pokemon == null)
        {
            return false;
        }
        
        // Check if other players have the pokemon
        var otherPlayerInventory = await _dbContext.PlayerInventories.FirstOrDefaultAsync(x => x.PokemonIdentifier.ToLower() == inventoryInput.PokemonIdentifier.ToLower());
        
        // If some other player has the pokemon then return false
        if (otherPlayerInventory != null)
        {
            return false;
        }
        
        // Check if the player has 10 pokemon, if so then return false
        var playerInventories = await _dbContext.PlayerInventories.Where(x => x.PlayerId == inventoryInput.PlayerId).ToListAsync();
        if (playerInventories.Count >= 10)
        {
            return false;
        }
        
        // Get Player
        var player = await _dbContext.Players.FirstOrDefaultAsync(x => x.Id == inventoryInput.PlayerId);
        
        // Add the pokemon to the player
        var playerInventoryEntity = new PlayerInventory
        {
            PlayerId = player.Id,
            //Player = player,
            PokemonIdentifier = inventoryInput.PokemonIdentifier.ToLower(),
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

        return false;
    }
    
    // Remove pokemonIdentifier from playerID
    public async Task<bool> RemovePokemonFromPlayer(InventoryInputModel inventoryInput)
    {
        // Check if the player exists and has the pokemon
        var playerInventory = _dbContext.PlayerInventories.FirstOrDefault(x => x.PlayerId == inventoryInput.PlayerId && x.PokemonIdentifier.ToLower() == inventoryInput.PokemonIdentifier.ToLower());
        if(playerInventory == null)
        {
            return false;
        }
        
        // Remove the pokemon from the player
        try{
            _dbContext.PlayerInventories.Remove(playerInventory);
            await _dbContext.SaveChangesAsync();
            return true;
        }catch{
            return false;
        }

        return false;
    }

    public async Task<PokemonDto> GetPokemonByName(string name)
    {
        var pokemon = await _pokemonService.GetPokemonByName(name);

        var player = await GetInventoryItemsByItemId(name);

        var owner = player.FirstOrDefault().Player;

        return new PokemonDto {
            name = pokemon.name,
            healthPoints = pokemon.healthPoints,
            baseAttack = pokemon.baseAttack,
            weight = pokemon.weight,
            owners = new PlayerDto {
                Id = owner.Id,
                Name = owner.Name,
                Deleted = owner.Deleted,
            },
        };
    }
}
