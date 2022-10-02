using GraphQL;
using Battleground.Api.Schema.Types;
using Battleground.Services.Interfaces;

using GraphQL.Types;

namespace Battleground.Api.Schema.Queries
{
    public class BattlegroundQuery : ObjectGraphType
    {
        private readonly IPokemonService _pokemonService;
        private readonly IPlayerService _playerService;
        private readonly IBattleService _battleService;
        public BattlegroundQuery(IPokemonService pokemonService, IPlayerService playerService, IBattleService battleService)
        {
            _pokemonService = pokemonService;
            _playerService = playerService;
            _battleService = battleService;

            Field<PokemonType>("pokemon")
            .Argument<StringGraphType>("name")
            .ResolveAsync(async context => {
            var name = context.GetArgument<string>("name");
            return await _pokemonService.GetPokemonByName(name);
            });
            

            Field<ListGraphType<PlayerType>>("allPlayers")
            .Resolve(context => _playerService.AllPlayers());
            
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

            
        }
    }
}