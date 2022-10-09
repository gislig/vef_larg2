using GraphQL;
using Battleground.Api.Schema.Types;
using Battleground.Repositories;
using Battleground.Repositories.Entities;
using Battleground.Services.Implementations;
using Battleground.Services.Interfaces;
using Battleground.Models.Dtos;
using Battleground.Models.Enums;

using GraphQL.Types;
using Microsoft.EntityFrameworkCore;

namespace Battleground.Api.Schema.Queries
{
    public class BattlegroundQuery : ObjectGraphType
    {
        private readonly IPokemonService _pokemonService;
        public BattlegroundQuery(IPokemonService pokemonService, IDefer<IPlayerService> playerService, IDefer<IBattleService> battleService, IDefer<IInventoryService> inventoryService, IDefer<IAttackService> attackService)
        {
            _pokemonService = pokemonService;

            Field<ListGraphType<PokemonType>>("allPokemons")
            .ResolveAsync(async context => {
                var pokemons = await _pokemonService.GetAllPokemons();
                var owner_arr = await inventoryService.Value.GetInventoryItems();
                var ret_value = new List<PokemonDto>();
                foreach (var pokemon in pokemons)
                {
                    var inventory_item = owner_arr.FirstOrDefault(p => p.PokemonIdentifier == pokemon.name);
                    if (inventory_item != null){
                        var owner = inventory_item.Player;
                        ret_value.Add(
                            new PokemonDto{
                                name = pokemon.name,
                                baseAttack = pokemon.baseAttack,
                                healthPoints = pokemon.healthPoints,
                                weight = pokemon.weight,
                                owners = new PlayerDto {
                                    Id = owner.Id,
                                    Name = owner.Name,
                                    Deleted = owner.Deleted
                                }}
                            ); 
                    } else {

                            ret_value.Add(
                            new PokemonDto{
                                name = pokemon.name,
                                baseAttack = pokemon.baseAttack,
                                healthPoints = pokemon.healthPoints,
                                weight = pokemon.weight,
                                owners = null
                            }
                            ); 
               
                        

                    }
                }
                return ret_value;
            });
            
            Field<ListGraphType<BattleType>>("allBattles")
            .ResolveAsync(async context => {
                var battles = await battleService.Value.AllBattles();
                var ret_value = new List<BattleDto>();
                foreach (var battle in battles) 
                {
                    // get the player
                    var players = new List<PlayerDto>();
                    foreach (var battlePlayer in battle.BattlePlayers)
                    {  
                        var player = await playerService.Value.GetPlayerById(battlePlayer.PlayerInMatchId);
                        players.Add(player);
                    }
                    // get the pokemons and attacks
                    var pokemons = new List<PokemonDto>();
                    var attacks = new List<AttackTypeDto>();
                    foreach (var pokemon in battle.BattlePokemons)
                    {
                        var pokemon_reps = await _pokemonService.GetPokemonByName(pokemon.PokemonIdentifier);
                        var p = new PokemonDto{
                            name = pokemon.PokemonIdentifier,
                            healthPoints = pokemon_reps.healthPoints,
                            baseAttack = pokemon_reps.baseAttack,
                            weight = pokemon_reps.weight,
                        };
                        
                        pokemons.Add(p);
                        
                        var attack = await attackService.Value.GetAttacksByBattlePokemons(pokemon.Id);
                        foreach( var a in attack) {
                            attacks.Add(new AttackTypeDto {
                                SuccessHit = a.Success,
                                CriticalHit = a.CriticalHit,
                                DamageDealt = a.Damage,
                                AttackedBy = p,
                            });
                        }
                    }

                    // populate return value
                    ret_value.Add(new BattleDto {
                        Id = battle.Id,
                        WinnerId = battle.WinnerId,
                        Winner = new PlayerDto {
                            Id = battle.Winner.Id,
                            Name = battle.Winner.Name
                        },
                        StatusId = battle.StatusId,
                        PlayersInMatch = players,
                        BattlePokemons = pokemons,
                        Attacks = attacks,
                    });
                }
                return ret_value;
            });


            Field<ListGraphType<PlayerType>>("allPlayers")
            .ResolveAsync(async context =>  await playerService.Value.AllPlayers());
            

            Field<PokemonType>("pokemon")
            .Argument<StringGraphType>("name")
            .ResolveAsync(async context => {
                PlayerDto owner = null;
                var name = context.GetArgument<string>("name");
                var pokemon = await _pokemonService.GetPokemonByName(name);
                var owner_arr = await inventoryService.Value.GetInventoryItemsByItemId(name);
                var inventory = owner_arr.FirstOrDefault();
                if(inventory != null)
                {
                    var player = inventory.Player;
                    owner = new PlayerDto {
                        Id = player.Id,
                        Name = player.Name,
                        Deleted = player.Deleted
                    };
                }

                return new PokemonDto{
                    name = pokemon.name,
                    baseAttack = pokemon.baseAttack,
                    healthPoints = pokemon.healthPoints,
                    weight = pokemon.weight,
                    owners = owner
                };
            
            });
            

            //TODO: NEED TO IMPLEMENT
            Field<BattleType>("battle")
            .Argument<IntGraphType>("id")
            .ResolveAsync(async context => {
                var id = context.GetArgument<int>("id");
                var battle = await battleService.Value.GetBattleById(id);
                // get the player
                    var players = new List<PlayerDto>();
                    foreach (var battlePlayer in battle.BattlePlayers)
                    {  
                        var player = await playerService.Value.GetPlayerById(battlePlayer.PlayerInMatchId);
                        players.Add(player);
                    }
                    // get the pokemons and attacks
                    var pokemons = new List<PokemonDto>();
                    var attacks = new List<AttackTypeDto>();
                    foreach (var pokemon in battle.BattlePokemons)
                    {
                        var pokemon_reps = await _pokemonService.GetPokemonByName(pokemon.PokemonIdentifier);
                        var p = new PokemonDto{
                            name = pokemon.PokemonIdentifier,
                            healthPoints = pokemon_reps.healthPoints,
                            baseAttack = pokemon_reps.baseAttack,
                            weight = pokemon_reps.weight,
                        };
                        
                        pokemons.Add(p);
                        
                        var attack = await attackService.Value.GetAttacksByBattlePokemons(pokemon.Id);
                        foreach( var a in attack) {
                            attacks.Add(new AttackTypeDto {
                                SuccessHit = a.Success,
                                CriticalHit = a.CriticalHit,
                                DamageDealt = a.Damage,
                                AttackedBy = p,
                            });
                        }
                    }

                    // populate return value
                    return new BattleDto {
                        Id = battle.Id,
                        WinnerId = battle.WinnerId,
                        Winner = new PlayerDto {
                            Id = battle.Winner.Id,
                            Name = battle.Winner.Name
                        },
                        StatusId = battle.StatusId,
                        PlayersInMatch = players,
                        BattlePokemons = pokemons,
                        Attacks = attacks,
                    };
            });

            Field<PlayerType>("player")
            .Argument<IntGraphType>("id")
            .ResolveAsync(async context => {
                var id = context.GetArgument<int>("id");
                return await playerService.Value.GetPlayerById(id);
            
            
            });                 
        }
    }
}