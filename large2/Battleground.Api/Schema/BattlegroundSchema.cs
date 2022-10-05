using Battleground.Api.Schema.Mutations;
using Battleground.Api.Schema.Queries;
using GraphQL.Instrumentation;
using Battleground.Api.Schema.Types;

namespace Battleground.Api.Schema;

public class BattlegroundSchema : GraphQL.Types.Schema
{
    public BattlegroundSchema(IServiceProvider provider)
        : base(provider)
    {
        Query = provider.GetRequiredService<BattlegroundQuery>();
        Mutation = provider.GetRequiredService<BattlegroundMutation>();

        // RegisterType(typeof(PokemonType));
        // RegisterType(typeof(PlayerType));
        
        // RegisterType(typeof(BattleType)); //something wrong with battletype schema
        // RegisterType(typeof(AttackType)); //Something Wrong with attacktype schema

        FieldMiddleware.Use(new InstrumentFieldsMiddleware());

        
    }
}
