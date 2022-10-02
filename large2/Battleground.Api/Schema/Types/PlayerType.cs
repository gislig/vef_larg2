
using Battleground.Models.Dtos;
using GraphQL.Types;

namespace Battleground.Api.Schema.Types
{
    public sealed class PlayerType : ObjectGraphType<PlayerDto>
    {
        
        public PlayerType()
        {
            Field(x => x.Id).Description("The id of the player.");
            Field(x => x.Name).Description("The name of the player.");
            // TODO: Field -> inventory: An array of PokemonType (where the array cannot be null nor the items within the array)
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<PokemonType>>>>("inventory")
                .Resolve(context => {
                    return null;
                });

        }
    }
} 