using Battleground.Models.Dtos;
using Battleground.Services.Interfaces;
using System.Net.Http.Json;

namespace Battleground.Services.Implementations;

public class PokemonService : IPokemonService
{
    private readonly HttpClient _httpClient;

    public PokemonService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

public async Task<IEnumerable<PokemonDto>?> GetAllPokemons() =>
        await _httpClient.GetFromJsonAsync<IEnumerable<PokemonDto>>("pokemons");
    public async Task<PokemonDto?> GetPokemonByName(string name) =>
        await _httpClient.GetFromJsonAsync<PokemonDto>($"pokemons/{name}");
}