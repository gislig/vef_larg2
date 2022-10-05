using GraphQL;
using Battleground.Api.Schema.Types;
using Battleground.Repositories;
using Battleground.Repositories.Entities;
using Battleground.Services.Implementations;
using Battleground.Services.Interfaces;

using GraphQL.Types;
using Microsoft.EntityFrameworkCore;

namespace Battleground.Api.Schema.Queries
{
    public class BattlegroundQuery : ObjectGraphType
    {
        private readonly IPokemonService _pokemonService;
        public BattlegroundQuery(IPokemonService pokemonService, IDefer<IPlayerService> playerService, IDefer<IBattleService> battleService)
        {
            _pokemonService = pokemonService;

            Field<ListGraphType<PokemonType>>("allPokemons")
            .ResolveAsync(async context => await _pokemonService.GetAllPokemons());
            
            Field<ListGraphType<BattleType>>("allBattles")
            .Resolve(context => battleService.Value.GetAllAttacks());

            Field<ListGraphType<PlayerType>>("allPlayers")
            .Resolve(context => playerService.Value.AllPlayers());

            Field<PokemonType>("pokemon")
            .Argument<StringGraphType>("name")
            .ResolveAsync(async context => {
            var name = context.GetArgument<string>("name");
            return await _pokemonService.GetPokemonByName(name);
            });

            //TODO: NEED TO IMPLEMENT
            Field<BattleType>("battle")
            .Argument<IntGraphType>("id")
            .Resolve( context => {
            var id = context.GetArgument<int>("id");
            return battleService.Value.GetAttackById(1);
            });

            // Field<PlayerType>("player")
            // .Argument<IntGraphType>("id")
            // .Resolve(context => {
            // var id = context.GetArgument<int>("id");
            // return playerService.Value.GetPlayerById(id);
            // });                 
        }
    }
}