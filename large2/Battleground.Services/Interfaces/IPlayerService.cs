using Battleground.Repositories.Entities;
using Microsoft.EntityFrameworkCore;
using Battleground.Services.Interfaces;
using Battleground.Models.Dtos;

namespace Battleground.Services.Interfaces;

public interface IPlayerService
{
    public Task<Player?> GetPlayerById(int id);
    public IEnumerable<PlayerDto?> AllPlayers();

    public Task<Player?> CreatePlayer(Player player);
    public Task<Player?> UpdatePlayer(Player player);
    public Task<Player?> DeletePlayer(int id);



}