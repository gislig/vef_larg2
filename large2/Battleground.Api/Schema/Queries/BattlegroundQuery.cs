using Battleground.Api.Schema.Types;
using Battleground.Repositories.Interfaces;
using Battleground.Services.Interfaces;
using GraphQL.Types;

namespace Battleground.Api.Schema.Queries
{
    public class BattlegroundQuery : ObjectGraphType
    {

        public BattlegroundQuery(IPokemonService pokemonService)
        {
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<PlayerType>>>>
            ("allPlayers")
                .Resolve(context => {
                    return null;
                });

            Field<PlayerType>
            ("player")
                .Argument<IntGraphType>("id")
                .Resolve(context => {
                    var idArgument = context.Arguments["id"];
                    return null;
                });
            
            
            // TODO: allBattles(status: BattleStatus): [BattleType!]! -> status:BattleStatus vantar!
            // Field<NonNullGraphType<ListGraphType<NonNullGraphType<BattleType>>>>
            // ("allBattles")
            //     // .Argument<Battle>("status")
            //     .Resolve(context => {
            //         // var statusArgument = context.Arguments["status"];
            //         return null;
            //     });

            // Field<BattleType>
            // ("Battle")
            //     .Argument<IntGraphType>("id")
            //     .Resolve(context => {
            //         var idArguments = context.Arguments["id"];
            //         return null;
            //     });

            
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<PokemonType>>>>
            ("allPokemons")
                .ResolveAsync(async context => await pokemonService.GetAllPokemons());

            Field<PokemonType>
            ("pokemon")
                .Argument<StringGraphType>("id")
                .Resolve(context => {
                    var idArgument = context.Arguments["id"];
                    return null;
             });
        }
    }
}