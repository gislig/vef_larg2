namespace Battleground.Repositories.Entities;

public class BattlePlayer
{
    [Key]
    public int Id { get; set; }
    
    [ForeignKey("Battle")]
    public int BattlesId { get; set; }
    
    [ForeignKey("Player")]
    public int PlayerInMatchId { get; set; }
}