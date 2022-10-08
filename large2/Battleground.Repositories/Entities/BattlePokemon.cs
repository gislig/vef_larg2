namespace Battleground.Repositories.Entities;

public class BattlePokemon
{
    [Key]
    public int Id { get; set; }
    
    [ForeignKey("Battle")]
    public int BattleId { get; set; }
    public Battle Battle { get; set; }
    
    public string PokemonIdentifier { get; set; }
}