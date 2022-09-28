using Battleground.Repositories.Entities;
using Battleground.Repositories.Interfaces;
using Newtonsoft.Json;

namespace Battleground.Repositories.Implementations;

public class PokemonRepository : IPokemonRepository
{
    // Gets all pokemons from the API and returns them as a model list Pokemon
    public async Task<IEnumerable<Pokemon>?> GetAllPokemons()
    {
        // Gets all pokemons from https://pokemon-proxy-api.herokuapp.com/pokemons
        HttpClient client = new HttpClient();
        var url = "https://pokemon-proxy-api.herokuapp.com/pokemons";
        var response = await client.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<IEnumerable<Pokemon>>(content);
    }
    
    // Gets a pokemon by name from the API and returns it as a model Pokemon
    public async Task<Pokemon?> GetPokemonByName(string name)
    {
        // Gets a pokemon by id from https://pokemon-proxy-api.herokuapp.com/pokemons/{name}
        HttpClient client = new HttpClient();
        var url = $"https://pokemon-proxy-api.herokuapp.com/pokemons/{name}";
        var response = await client.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<Pokemon>(content);
    }
}