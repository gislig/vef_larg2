using Battleground.Repositories.Entities;

namespace Battleground.Repositories.Interfaces;

public interface IPlayerRepository
{
    Task<Player?> GetPlayerById(int id);
    Task<IEnumerable<Player?>> AllPlayers();
    Task<Player?> CreatePlayer(Player player);
    Task<Player?> UpdatePlayer(Player player);
    Task<Player?> DeletePlayer(int id);
}