using Battleground.Models.Models;

namespace Battleground.Services.Interfaces;

public interface IPokemonService
{
    Task<IEnumerable<PokemonModel>?> GetAllPokemons();
    Task<PokemonModel?> GetPokemonByName(string name);
}