namespace Battleground.Models.Dtos;

public class PlayerDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool Deleted { get; set; }
    public PokemonDto inventory { get; set; }

    public IEnumerable<string> pokemonIdentifier{ get; set; } = null!;
}