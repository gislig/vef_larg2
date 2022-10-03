
using Battleground.Models.Dtos;
using GraphQL.Types;

namespace Battleground.Api.Schema.Types
{
    public class PokemonType : ObjectGraphType<PokemonDto>
    {
        public PokemonType()
        {
            Field(x => x.name).Description("The name of the Pokemon.");
            Field(x => x.baseAttack).Description("Attack strength.");
            Field(x => x.healthPoints).Description("Health of the Pokemon.");
            Field(x => x.weight).Description("The weight of the Pokemon.");
            // Field<NonNullGraphType<ListGraphType<NonNullGraphType<PlayerType>>>>("owners")
            //     .Resolve(context => {
            //         return null;
            //     });
        }
    }
}