using Battleground.Repositories.Entities;
using GraphQL.Types;

namespace Battleground.Api.Schema.Types
{
    public class PokemonType : ObjectGraphType<Pokemon>
    {
        public PokemonType()
        {
            Field(x => x.Name).Description("The name of the Pokemon.");
            Field(x => x.BaseAttack).Description("Attack strength.");
            Field(x => x.HealthPoints).Description("Health of the Pokemon.");
            Field(x => x.Weight).Description("The weight of the Pokemon.");
            // TODO: Field -> owners: An array of PlayerType (where the array cannot be null nor the items within the array)
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<PlayerType>>>>("owners")
                .Resolve(context => {
                    return null;
                });
        }
    }
}