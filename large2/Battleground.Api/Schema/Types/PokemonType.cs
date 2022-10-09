using GraphQL.Types;
using Battleground.Models.Dtos;
namespace Battleground.Api.Schema.Types;

public sealed class PokemonType : ObjectGraphType<PokemonDto>
{
    public PokemonType()
    {
        Field(x => x.name).Description("The name of the pokemon");
        Field(x => x.baseAttack).Description("The base attack of the pokemon");
        Field(x => x.healthPoints).Description("The health points of the pokemon");
        Field(x => x.weight).Description("The weight of the pokemon");     
    }
}