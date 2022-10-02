using Battleground.Repositories.Entities;

namespace Battleground.Services.Interfaces;

public interface IInventoryService
{
    Task RemovePokemonFromPlayer(int playerId, string pokemonIdentifier);
    Task AddPokemonToPlayer(int playerId, string pokemonIdentifier);
    Task<List<PlayerInventory>> GetInventoryItemsByPlayerIdAndItemId(int playerId, string pokemonIdentifier);
    Task<List<PlayerInventory>> GetInventoryItemsByItemId(string pokemonIdentifier);
    Task<List<PlayerInventory>> GetInventoryItemsByPlayerId(int playerId);
    Task<List<PlayerInventory>> GetInventoryItems();
}