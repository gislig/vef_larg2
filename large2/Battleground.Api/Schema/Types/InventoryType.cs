using Battleground.Repositories.Entities;
using GraphQL.Types;
using Npgsql.PostgresTypes;

namespace Battleground.Api.Schema.Types
{
    public class InventoryType : ObjectGraphType<PlayerInventory>
    {
        public InventoryType()
        {
            Field(x => x.Id).Description("The id of the Battle.");  
            // Field(x => x.PokemonIdentifier).Description("The Id of the winner for this battle");

        }
    }
}