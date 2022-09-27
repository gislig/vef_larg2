namespace Battleground.Repositories.Entities;

public class Players
{
    [Key, Required]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public bool Deleted { get; set; }
}