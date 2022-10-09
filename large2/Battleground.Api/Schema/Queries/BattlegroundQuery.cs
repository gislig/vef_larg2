using GraphQL;
using Battleground.Api.Schema.Types;
using Battleground.Repositories;
using Battleground.Repositories.Entities;
using Battleground.Services.Implementations;
using Battleground.Services.Interfaces;
using Battleground.Models.Dtos;

using GraphQL.Types;
using Microsoft.EntityFrameworkCore;

namespace Battleground.Api.Schema.Queries
{
    public class BattlegroundQuery : ObjectGraphType
    {
        private readonly IPokemonService _pokemonService;
        public BattlegroundQuery(IPokemonService pokemonService, IDefer<IPlayerService> playerService, IDefer<IBattleService> battleService, IDefer<IInventoryService> inventoryService)
        {
            _pokemonService = pokemonService;

            Field<ListGraphType<PokemonType>>("allPokemons")
            .ResolveAsync(async context => await _pokemonService.GetAllPokemons());
            
            Field<ListGraphType<BattleType>>("allBattles")
            .Resolve(context => battleService.Value.AllBattles());


            Field<ListGraphType<PlayerType>>("allPlayers")
            .ResolveAsync(async context =>  await playerService.Value.AllPlayers());
            

            Field<PokemonType>("pokemon")
            .Argument<StringGraphType>("name")
            .ResolveAsync(async context => {
                var name = context.GetArgument<string>("name");
                var pokemon = await _pokemonService.GetPokemonByName(name);
                var owner_arr = await inventoryService.Value.GetInventoryItemsByItemId(name);
                var owner = owner_arr.First().Player;

                return new PokemonDto{

                    name = pokemon.name,
                    baseAttack = pokemon.baseAttack,
                    healthPoints = pokemon.healthPoints,
                    weight = pokemon.weight,
                    owners = new PlayerDto {
                        Id = owner.Id,
                        Name = owner.Name,
                        Deleted = owner.Deleted
                    }
                };
            
            });

            //TODO: NEED TO IMPLEMENT
            Field<BattleType>("battle")
            .Argument<IntGraphType>("id")
            .ResolveAsync(async context => {
            var id = context.GetArgument<int>("id");
            return await battleService.Value.GetBattleById(1);
            });

            Field<PlayerType>("player")
            .Argument<IntGraphType>("id")
            .ResolveAsync(async context => {
            var id = context.GetArgument<int>("id");
            var player = await playerService.Value.GetPlayerById(id);
            var pokemons_arr = await inventoryService.Value.GetInventoryItemsByPlayerId(id);

            return new PlayerDto{
                Id = player.Id,
                Name = player.Name,
                Deleted = player.Deleted,
                inventory = new PokemonDto{
                    name = pokemons_arr.First().PokemonIdentifier,
                    baseAttack = 0,
                    healthPoints = 0,
                    weight = 0,
                    owners = new PlayerDto{
                        Id = player.Id,
                        Name = player.Name,
                        Deleted = player.Deleted
                    }
                }
            };
            });
            


            // var id = context.GetArgument<int>("id");
            // return await playerService.Value.GetPlayerById(id);
            
            
        }                 
    }
}
