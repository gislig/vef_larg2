namespace Battleground.Repositories.Entities;

public class PlayerInventory
{
    [Key]
    public int Id { get; set; }
    
    [ForeignKey("Player")]
    public int PlayerId { get; set; }
    public Player? Player { get; set; }
    
    public DateTime AcquiredDate { get; set; }

    public string PokemonIdentifier { get; set; }

}