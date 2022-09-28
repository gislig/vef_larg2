using Battleground.Repositories.Entities;

namespace Battleground.Repositories.Interfaces;

public interface IPokemonRepository
{
    Task<IEnumerable<Pokemon>> GetAllPokemons();
    Task<Pokemon> GetPokemonByName(string name);
}