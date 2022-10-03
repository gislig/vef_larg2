using System.Text.Json;
using Battleground.Models.Models;
using Battleground.Services.Interfaces;

namespace Battleground.Services.Implementations;

public class PokemonService : IPokemonService
{
    private readonly HttpClient _httpClient;

    public PokemonService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Gets all pokemons from the API and returns them as a model list Pokemon
    public async Task<IEnumerable<PokemonModel>?> GetAllPokemons()
    {
        var response = await _httpClient.GetAsync("pokemons");
        if(!response.IsSuccessStatusCode)
        {
            throw new Exception("Could not get pokemons");
        }

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<IEnumerable<PokemonModel>>(content);
    }
    
    // Gets a pokemon by name from the API and returns it as a model Pokemon
    public async Task<PokemonModel?> GetPokemonByName(string name)
    {
        var response = await _httpClient.GetAsync($"pokemon/{name}");
        if(!response.IsSuccessStatusCode)
        {
            throw new Exception("Could not get pokemon");
        }

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<PokemonModel>(content);
    }
}