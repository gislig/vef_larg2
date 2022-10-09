using Battleground.Models.Dtos;
using Battleground.Services.Interfaces;
using System.Net.Http.Json;

namespace Battleground.Services.Implementations;

// TODO: Finna stað fyrir þennan


public class PokemonService : IPokemonService
{
    private readonly HttpClient _httpClient;


    public PokemonService(HttpClient httpClient )
    {
        _httpClient = httpClient;
        // _inventoryService = inventoryService;
    }

    public async Task<IEnumerable<PokemonDto>?> GetAllPokemons() =>
        await _httpClient.GetFromJsonAsync<IEnumerable<PokemonDto>>("pokemons");
    public async Task<PokemonResponseDto?> GetPokemonByName(string name)
    {
        return await _httpClient.GetFromJsonAsync<PokemonResponseDto>($"pokemons/{name}");


    }
}