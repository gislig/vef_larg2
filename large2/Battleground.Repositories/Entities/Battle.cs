namespace Battleground.Repositories.Entities;

public class Battle
{
    [Key]
    public int Id { get; set; }
    
    public int WinnerId { get; set; }
    
    [ForeignKey("BattleStatus")]
    public int StatusId { get; set; }
}