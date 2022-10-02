using Battleground.Repositories.Entities;
using Microsoft.EntityFrameworkCore;
using Battleground.Services.Interfaces;
using Battleground.Models.Dtos;

namespace Battleground.Services.Interfaces;

public interface IPlayerService
{
    Task<Player?> GetPlayerById(int id);
    Task<IEnumerable<Player?>> AllPlayers();

    Task<Player?> CreatePlayer(Player player);
    Task<Player?> UpdatePlayer(Player player);
    Task<Player?> DeletePlayer(int id);



}