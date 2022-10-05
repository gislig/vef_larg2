using Battleground.Repositories.Entities;
using Microsoft.EntityFrameworkCore;
using Battleground.Services.Interfaces;
using Battleground.Models.Dtos;
using Battleground.Models.InputModels;

namespace Battleground.Services.Interfaces;

public interface IPlayerService
{
    Player? GetPlayerById(int id);
    IEnumerable<Player?> AllPlayers();
    PlayerDto? CreatePlayer(PlayerInputModel player);
    Player? UpdatePlayer(Player player);
    Player? DeletePlayer(int id);
}