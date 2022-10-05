namespace Battleground.Repositories.Entities;

public class Battle
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public int WinnerId { get; set; } // TODO: Hvað er þetta?
    
    [ForeignKey("BattleStatus")]
    public int StatusId { get; set; }
}