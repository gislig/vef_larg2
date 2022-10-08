namespace Battleground.Repositories.Entities;

public class Battle
{
    [Key]
    public int Id { get; set; }
    
    [ForeignKey("Player")]
    public int WinnerId { get; set; }
    public Player Winner { get; set; }
    
    [ForeignKey("BattleStatus")]
    public int StatusId { get; set; }
    public BattleStatus BattleStatus { get; set; }
}