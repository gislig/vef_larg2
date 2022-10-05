using Battleground.Repositories.Entities;
using GraphQL.Types;
using Npgsql.PostgresTypes;

namespace Battleground.Api.Schema.Types
{
    public class BattleType : ObjectGraphType<Battle>
    {
        public BattleType()
        {
            Field(b => b.Id).Description("The ID of the battle.");
            
            Field(b => b.StatusId).Description("The status of the battle.");
            Field<PlayerType>("winner").Resolve(context => null);
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<PokemonType>>>>("battlePokemons")
                .Resolve(context => null);
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<PlayerType>>>>("playersInMatch")
                .Resolve(context => null);
            //Field<NonNullGraphType<ListGraphType<NonNullGraphType<AttackType>>>>("attacks")
            //    .Resolve(context => null);
        }
    }
}
