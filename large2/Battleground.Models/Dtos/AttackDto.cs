namespace Battleground.Models.Dtos;

public class AttackDto
{
    public int Id { get; set; }
    public bool Success { get; set; }
    public int CriticalHit { get; set; }
    public int Damage { get; set; }
    public int BattlePokemonId { get; set; }
    public string PokemonIdentifier { get; set; }
    public int BattleId { get; set; }
    public int AttackerId { get; set; }
}