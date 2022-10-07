using GraphQL.Types;
using Battleground.Repositories.Entities;
namespace Battleground.Api.Schema.Types;

public sealed class PokemonType : ObjectGraphType<Pokemon>
{
    public PokemonType()
    {
        Field(x => x.name).Description("The name of the pokemon");
        Field(x => x.baseAttack).Description("The base attack of the pokemon");
        Field(x => x.healthPoints).Description("The health points of the pokemon");
        Field(x => x.weight).Description("The weight of the pokemon");
        
        
        
        // Field(x => x.owners).Description("Owners that own this pokemon");
        //TODO NEED TO FIX 
        // Field<PlayerType>("player")
        //     .Description("Players that own this pokemon")
        //     .Resolve(context => {
        //         var data = inventoryService.Value.AllPlayers();
        //         return data;
        //     });          
    }
}