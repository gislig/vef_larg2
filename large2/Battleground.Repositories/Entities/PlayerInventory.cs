namespace Battleground.Repositories.Entities;

public class PlayerInventory
{
    [Key]
    public int Id { get; set; }
    
    [ForeignKey("Player")]
    public int PlayerId { get; set; }
    
    public DateTime AcquiredDate { get; set; }
    
    public int PokemonIdentifier { get; set; } // TODO: Hvað er þetta?
}