using System.ComponentModel.DataAnnotations;

namespace Battleground.Models.InputModels;

public class AttackInputModel
{
    [Required]
    public int AttackerId { get; set; }
    [Required]
    public int BattleId { get; set; }
}