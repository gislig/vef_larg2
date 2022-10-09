using Battleground.Repositories.Entities;
using Microsoft.EntityFrameworkCore;
using Battleground.Services.Interfaces;
using Battleground.Models.Dtos;
using Battleground.Models.InputModels;

namespace Battleground.Services.Interfaces;

public interface IPlayerService
{
    Task<PlayerDto?> GetPlayerById(int id);
    Task<IEnumerable<PlayerDto?>> AllPlayers();
    Task<PlayerDto?> CreatePlayer(PlayerInputModel player);
    Task<Player?> UpdatePlayer(Player? player);
    Task<bool> RemovePlayer(int id);
}