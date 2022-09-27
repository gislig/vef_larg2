namespace Battleground.Repositories.Entities;

public class BattlePlayer
{
    [ForeignKey("Battle")]
    public int BattlesId { get; set; }
    
    [ForeignKey("Player")]
    public int PlayersInMatchId { get; set; }
}