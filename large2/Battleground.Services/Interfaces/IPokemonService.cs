

using Battleground.Models.Dtos;

namespace Battleground.Services.Interfaces;

public interface IPokemonService
{
    Task<IEnumerable<PokemonDto>?> GetAllPokemons();
    Task<PokemonDto?> GetPokemonByName(string name);
}