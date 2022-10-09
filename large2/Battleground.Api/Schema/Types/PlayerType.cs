
using Battleground.Models.Dtos;
using Battleground.Services.Interfaces;
using GraphQL.Types;

namespace Battleground.Api.Schema.Types
{
    public sealed class PlayerType : ObjectGraphType<PlayerDto>
    {
        
        
        public PlayerType()
        {
            Field(x => x.Id).Description("The id of the player.");
            Field(x => x.Name).Description("The name of the player.");
            Field(x => x.Deleted).Description("is the player active?");
            Field(x => x.pokemonIdentifier).Description("Pokemons in this player inventory");
            Field(x => x.inventory, type: typeof(InventoryType)).Description("The inventory of the player");
            // Field<InventoryType>("owners").

//         id: ID*
// ■name: string*
// ■inventory: An array of PokemonType (where the array cannot be null
// nor the items within the array)
            
            // Field<NonNullGraphType<ListGraphType<InventoryType>>>("inventory").Description("Pokemon Inventory of player")
            // .Resolve(context => inventoryService.Value.GetInventoryItemsByPlayerId(1));
            // TODO ADD INVENTORY
            // Field<NonNullGraphType<ListGraphType<NonNullGraphType<PokemonType>>>>("inventory")
            //     .Resolve(context. => {
            //         return null;
            //     });

        }
    }
} 