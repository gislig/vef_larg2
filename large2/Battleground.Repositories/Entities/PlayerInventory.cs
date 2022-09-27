namespace Battleground.Repositories.Entities;

public class PlayerInventory
{
    [ForeignKey("Player")]
    public int PlayerId { get; set; }
    
    public DateTime AcquiredDate { get; set; }
    
    public int PokemonIdentifier { get; set; } // TODO: Hvað er þetta?
}