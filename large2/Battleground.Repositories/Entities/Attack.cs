namespace Battleground.Repositories.Entities;

public class Attack
{
    [Key]
    public int Id { get; set; }
    public bool Success { get; set; }
    public int CriticalHit { get; set; }
    public int Damage { get; set; }
    
    [ForeignKey("BattlePokemon")]
    public int BattlePokemonId { get; set; }
    public BattlePokemon BattlePokemon { get; set; }
    
    public int PokemonIdentifier { get; set; }
}