using Battleground.Models.InputModels;
using Battleground.Repositories.Entities;

namespace Battleground.Services.Interfaces;

public interface IInventoryService
{
    bool RemovePokemonFromPlayer(InventoryInputModel inventoryInput);
    bool AddPokemonToPlayer(InventoryInputModel inventoryInput);
    // IEnumerable<PlayerInventory> GetInventoryItemsByPlayerIdAndItemId(int playerId, string pokemonIdentifier);
    IEnumerable<PlayerInventory> GetInventoryItemsByItemId(string pokemonIdentifier);
    IEnumerable<PlayerInventory> GetInventoryItemsByPlayerId(int playerId);
    IEnumerable<PlayerInventory> GetInventoryItems();
}