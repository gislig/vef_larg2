using GraphQL;
using Battleground.Api.Schema.Types;
using Battleground.Repositories;
using Battleground.Repositories.Entities;
using Battleground.Services.Interfaces;

using GraphQL.Types;
using Microsoft.EntityFrameworkCore;

namespace Battleground.Api.Schema.Queries
{
    public class BattlegroundQuery : ObjectGraphType
    {
        // TODO: Ef DbContext er í þessum þá fer allt í kakó, hvort sem það er í service eða í þessum klasa.
        //private readonly BattlegroundDbContext _dbContext;
        
        private readonly IPlayerService _playerService;
        private readonly IPokemonService _pokemonService;
        private readonly IBattleService _battleService;
        private readonly IInventoryService _inventoryService;

        public BattlegroundQuery(IPokemonService pokemonService)
        {
            //_dbContext = dbContext;
            //_playerService = playerService;
            _pokemonService = pokemonService;
            //_battleService = battleService;
            //_inventoryService = inventoryService;

            Field<ListGraphType<PlayerType>>("playersFromDB", resolve: context =>
            { 
                var playerContext = context.RequestServices.GetRequiredService<BattlegroundDbContext>();
                var players = playerContext.Players.ToList();

                return players;
            });
                
            
            Field<ListGraphType<PokemonType>>("allPokemons")
            .ResolveAsync(async context => await _pokemonService.GetAllPokemons());

            Field<PokemonType>("pokemon")
            .Argument<StringGraphType>("name")
            .ResolveAsync(async context => {
            var name = context.GetArgument<string>("name");
            return await _pokemonService.GetPokemonByName(name);
            
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

            
        }
    }
}