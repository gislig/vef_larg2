using Battleground.Repositories.Entities;
using Microsoft.EntityFrameworkCore;
using Battleground.Services.Interfaces;
using Battleground.Models.Dtos;

namespace Battleground.Services.Interfaces;

public interface IPlayerService
{
    Player? GetPlayerById(int id);
    IEnumerable<Player?> AllPlayers();
    Player? CreatePlayer(Player player);
    Player? UpdatePlayer(Player player);
    Player? DeletePlayer(int id);
}