using Battleground.Models.InputModels;
using GraphQL.Types;
using Npgsql.PostgresTypes;

namespace Battleground.Api.Schema.InputTypes;

public class InventoryInputType : InputObjectGraphType<InventoryInputModel>
{
    public InventoryInputType()
    {
        Name = "AddInventoryToPlayerInput";
        Field(b => b.PokemonIdentifier).Description("The identifier of the pokemon to add to the player's inventory.");
        Field(b => b.PlayerId).Description("The identifier of the player to add the pokemon to.");
    }
}