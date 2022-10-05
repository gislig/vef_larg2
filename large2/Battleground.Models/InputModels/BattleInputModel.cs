namespace Battleground.Models.InputModels;

public class BattleInputModel
{
    public IEnumerable<int> Players { get; set; }
    public IEnumerable<int> Pokemons { get; set; }
}