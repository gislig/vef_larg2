namespace Battleground.Repositories.Entities;

public class BattleStatus
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
}