using Battleground.Repositories.Entities;
using Microsoft.EntityFrameworkCore;
using Battleground.Services.Interfaces;
using Battleground.Repositories;
using Battleground.Models.Dtos;
using Battleground.Models.InputModels;

namespace Battleground.Services.Implementations;

public class PlayerService : IPlayerService
{
    
    
    private readonly BattlegroundDbContext _dbContext;
    public PlayerService(BattlegroundDbContext context)
    {
        _dbContext = context;
    }

    // (5%) player - Should return a specific player by id
    public async Task<PlayerDto?> GetPlayerById(int id)
    {
        var player = _dbContext.Players.FirstOrDefault(p => p.Id == id);
        if(player != null)
            return new PlayerDto {
                Id = player.Id,
                Name = player.Name,
                Deleted = player.Deleted
            };
        return null;
    }

    // (5%) allPlayers - Should return a collection of all players
    public async Task<IEnumerable<PlayerDto?>> AllPlayers()
    {  
        return _dbContext.Players.Select(p => new PlayerDto {
            Id = p.Id,
            Name = p.Name,
            Deleted = p.Deleted
        });
    }   
    

    // Create a new player
    public async Task<PlayerDto?> CreatePlayer(PlayerInputModel player)
    {
        Player newPlayer = new Player()
        {
            Name = player.Name,
            Deleted = false
        };
        
        _dbContext.Players.Add(newPlayer);
        _dbContext.SaveChanges();
        
        PlayerDto playerDto = new PlayerDto()
        {
            Id = newPlayer.Id,
            Name = newPlayer.Name,
            Deleted = newPlayer.Deleted
        };
        
        return playerDto;
    }

    // Update Player
    public async Task<Player?> UpdatePlayer(Player player)
    {
        
        _dbContext.Players.Update(player);
        _dbContext.SaveChanges();
        return player;
    }

    // Delete Player
    public async Task<bool> RemovePlayer(int id)
    {
        var playerToUpdate = _dbContext.Players.FirstOrDefault(p => p.Id == id);
        if(playerToUpdate != null)
        {
            playerToUpdate.Deleted = true;
            _dbContext.Players.Update(playerToUpdate);
            _dbContext.SaveChanges();
            return true;
        }
        return false;
    }
}