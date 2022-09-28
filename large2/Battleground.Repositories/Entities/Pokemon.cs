namespace Battleground.Repositories.Entities;

// https://pokemon-proxy-api.herokuapp.com/pokemons
public class Pokemon
{
    public string Name { get; set; }
    public int HealthPoints { get; set; }
    public int BaseAttack { get; set; }
    public int Weight { get; set; }
}