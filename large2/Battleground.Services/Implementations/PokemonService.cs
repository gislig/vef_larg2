using Battleground.Models.Models;
using Battleground.Repositories.Interfaces;
using Battleground.Services.Interfaces;

namespace Battleground.Services.Implementations;

public class PokemonService : IPokemonService
{
    private readonly IPokemonRepository _pokemonRepository;

    public PokemonService(IPokemonRepository pokemonRepository)
    {
        _pokemonRepository = pokemonRepository;
    }

    public async Task<IEnumerable<PokemonModel>?> GetAllPokemons()
    {
        return await _pokemonRepository.GetAllPokemons();
    }

    public async Task<PokemonModel?> GetPokemonByName(string name)
    {
        return await _pokemonRepository.GetPokemonByName(name);
    }
}