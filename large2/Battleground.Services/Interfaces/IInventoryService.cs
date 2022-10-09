using Battleground.Models.InputModels;
using Battleground.Repositories.Entities;

namespace Battleground.Services.Interfaces;

public interface IInventoryService
{
    Task<bool> RemovePokemonFromPlayer(InventoryInputModel inventoryInput);
    Task<bool> AddPokemonToPlayer(InventoryInputModel inventoryInput);
    // IEnumerable<PlayerInventory> GetInventoryItemsByPlayerIdAndItemId(int playerId, string pokemonIdentifier);
    Task<IEnumerable<PlayerInventory>> GetInventoryItemsByItemId(string pokemonIdentifier);
    Task<IEnumerable<PlayerInventory>> GetInventoryItemsByPlayerId(int playerId);
    Task<IEnumerable<PlayerInventory>> GetInventoryItems();
}