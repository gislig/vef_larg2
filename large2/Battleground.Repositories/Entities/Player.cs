using Battleground.Models.Dtos;
namespace Battleground.Repositories.Entities;

public class Player
{
    [Key, Required]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public bool Deleted { get; set; }
    //public string pokemonIdentifier {get; set; } // ÞETTA ER BANNAÐ!!
}