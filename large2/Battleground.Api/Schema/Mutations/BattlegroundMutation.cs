using Battleground.Api.Schema.InputTypes;
using Battleground.Api.Schema.Types;
using Battleground.Models.InputModels;
using Battleground.Services.Interfaces;
using GraphQL;
using GraphQL.Types;

namespace Battleground.Api.Schema.Mutations
{
    public class BattlegroundMutation : ObjectGraphType
    {
        public BattlegroundMutation(
            IDefer<IBattleService> battleService, 
            IDefer<IPlayerService> playerService, 
            IDefer<IInventoryService> inventoryService, 
            IDefer<IAttackService> attackService)
        {
            // TODO : addBattle - Create a battle between two players pokémons and returns the newly created battle
            Field<NonNullGraphType<BattleType>>("addBattle")
                .Argument<NonNullGraphType<BattleInputType>>("inputBattle")
                .ResolveAsync(async context => {
                    BattleInputModel battle = context.GetArgument<BattleInputModel>("inputBattle");
                    
                    // use battleService to create a battle
                    var battleResults = await battleService.Value.CreateBattle(battle);
                    
                    // If battleResults are null then throw exception
                    if (battleResults.Id == null)
                    {
                        throw new ExecutionError("Battle could not be created");
                    }
                    
                    return battleResults;
                });
            
            
            // TODO : attack - Attacks a pokemon within a battle and returns the result
            Field<NonNullGraphType<AttackType>>("attack")
                .Argument<NonNullGraphType<AttackInputType>>("inputAttack")
                .ResolveAsync(async context => {
                    AttackInputModel attack = context.GetArgument<AttackInputModel>("inputAttack");
                    Console.WriteLine("Trying to attack");
                    // attack a pokemon
                    var attackResults = await attackService.Value.Attack(attack);
                    Console.WriteLine("Did I attack?");

                    // If attackResults are null then throw exception

                    return attackResults;
                });
            
            // TODO : addPlayer - Create a player and return the newly created player matching the Player type
            Field<NonNullGraphType<PlayerType>>("addPlayer")
                .Argument<NonNullGraphType<PlayerInputType>>("inputPlayer")
                .ResolveAsync(async context => {
                    PlayerInputModel player = context.GetArgument<PlayerInputModel>("inputPlayer");
                    
                    // use playerService to create a player
                    var playerResults = await playerService.Value.CreatePlayer(player);
                    // convert playerResults to PlayerType
                    return playerResults;
                });
            
            // TODO : addPokemonToInventory - Add a pokémon to an
            // inventory of a specific player and returns either
            // true or an error if something happened.
            // A player can only have one of each type -
            // therefore no duplicates allowed in the inventory
            Field<BooleanGraphType>("addPokemonToInventory")
                .Argument<NonNullGraphType<InventoryInputType>>("inputInventory")
                .ResolveAsync(async context => {
                    InventoryInputModel inventory = context.GetArgument<InventoryInputModel>("inputInventory");
                    // use inventoryService to create a player
                    var inventoryResults = await inventoryService.Value.AddPokemonToPlayer(inventory);
                    // convert playerResults to PlayerType
                    
                    // throw InventoryException IF false returns else return inventoryResults
                    return inventoryResults;
                });
            
            // TODO : removePokemonFromInventory - Removes a pokémon from an inventory of a specific
            // player and returns either true or an error if something
            // happened
            Field<BooleanGraphType>("removePokemonFromInventory")
                .Argument<NonNullGraphType<InventoryInputType>>("inputInventory")
                .ResolveAsync(async context => {
                    InventoryInputModel inventory = context.GetArgument<InventoryInputModel>("inputInventory");
                    // use inventoryService to create a player
                    var inventoryResults = await inventoryService.Value.RemovePokemonFromPlayer(inventory);
                    // convert playerResults to PlayerType
                    
                    // throw InventoryException IF false returns else return inventoryResults
                    return inventoryResults;
                });
            
            // TODO : removePlayer
            Field<BooleanGraphType>("removePlayer")
                .Argument<NonNullGraphType<IntGraphType>>("id")
                .ResolveAsync(async context => {
                    var player = context.GetArgument<int>("id");
                    // use playerService to create a player
                    var playerResults = await playerService.Value.RemovePlayer(player);
                    // convert playerResults to PlayerType
                    
                    // throw InventoryException IF false returns else return inventoryResults
                    return playerResults;
                });
        }        
    }
}