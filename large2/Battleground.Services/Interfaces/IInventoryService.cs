using Battleground.Repositories.Entities;

namespace Battleground.Services.Interfaces;

public interface IInventoryService
{
    bool RemovePokemonFromPlayer(int playerId, string pokemonIdentifier);
    bool AddPokemonToPlayer(int playerId, string pokemonIdentifier);
    // IEnumerable<PlayerInventory> GetInventoryItemsByPlayerIdAndItemId(int playerId, string pokemonIdentifier);
    IEnumerable<PlayerInventory> GetInventoryItemsByItemId(string pokemonIdentifier);
    IEnumerable<PlayerInventory> GetInventoryItemsByPlayerId(int playerId);
    IEnumerable<PlayerInventory> GetInventoryItems();
}