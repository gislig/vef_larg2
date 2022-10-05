using Battleground.Models.InputModels;
using GraphQL.Types;

namespace Battleground.Api.Schema.InputTypes;

public class BattleInputType : InputObjectGraphType<BattleInputModel>
{
    public BattleInputType()
    {
        Name = "BattleInputOfPlayersAndPokemons";
        Field(x => x.Players)
            .Description("List of playerIds");
        Field(x => x.Pokemons)
            .Description("List of pokemonIds");
    }
}