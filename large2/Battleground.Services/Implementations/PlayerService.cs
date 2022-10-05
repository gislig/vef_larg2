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
    public Player? GetPlayerById(int id)
    {
        return _dbContext.Players.Find(id);
    }

    // (5%) allPlayers - Should return a collection of all players
    public IEnumerable<Player?> AllPlayers()
    {
        return _dbContext.Players.ToList();
    }

    // Create a new player
    public PlayerDto? CreatePlayer(PlayerInputModel player)
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
    public Player? UpdatePlayer(Player player)
    {
        _dbContext.Players.Update(player);
        _dbContext.SaveChanges();
        return player;
    }

    // Delete Player
    public Player? DeletePlayer(int id)
    {
        var player = _dbContext.Players.Find(id);
        _dbContext.Players.Remove(player);
        _dbContext.SaveChangesAsync();
        return player;
    }
}