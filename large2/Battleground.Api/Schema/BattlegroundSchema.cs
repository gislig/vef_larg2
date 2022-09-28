using System;
using GraphQL;
using GraphQL.Instrumentation;

namespace Battleground.Api.Schema;

public class BattlegroundSchema : GraphQL.Types.Schema
{
    public BattlegroundSchema(IServiceProvider provider)
        : base(provider)
    {
        FieldMiddleware.Use(new InstrumentFieldsMiddleware());
    }
    
    /*
     Queries
        ○ pokemon(id: String): PokemonType
        ○ allPokemons: [PokemonType!]!
        ○ player(id: Int): PlayerType
        ○ allPlayers: [PlayerType!]!
        ○ battle(id: Int): BattleType
        ○ allBattles(status: BattleStatus): [BattleType!]!
      
     */
}