using Battleground.Models.Enums;
namespace Battleground.Models.Dtos;

public class BattleDto
{
    public int Id { get; set; }
    public int WinnerId { get; set; }

    public PlayerDto? Winner { get; set; }
    public int StatusId { get; set; }

    public IEnumerable<PlayerDto> PlayersInMatch { get; set; }

    public IEnumerable<PokemonDto> BattlePokemons { get; set; }

    public IEnumerable<AttackTypeDto> Attacks { get; set; }

    // public IEnumerable<AttackDto> Attacks { get; set; }
}