namespace Battleground.Repositories.Entities;

public class BattlePokemon
{
    [ForeignKey("Battle")]
    public int BattleId { get; set; }
    
    public int PokemonIdentifier { get; set; } // TODO: Hvað er þetta?
}