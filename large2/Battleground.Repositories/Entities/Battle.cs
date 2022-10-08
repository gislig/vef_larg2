namespace Battleground.Repositories.Entities;

public class Battle
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Player")] 
    public int WinnerId { get; set; } = 0;
    public Player? Winner { get; set; } = null;
    
    [ForeignKey("BattleStatus")]
    public int StatusId { get; set; }
    public BattleStatus BattleStatus { get; set; }
}