using Battleground.Api.Schema.Mutations;
using Battleground.Api.Schema.Queries;
using GraphQL.Instrumentation;

namespace Battleground.Api.Schema;

public class BattlegroundSchema : GraphQL.Types.Schema
{
    public BattlegroundSchema(IServiceProvider provider)
        : base(provider)
    {
        Query = provider.GetRequiredService<BattlegroundQuery>();
        Mutation = provider.GetRequiredService<BattlegroundMutation>();
        
        FieldMiddleware.Use(new InstrumentFieldsMiddleware());
    }
}
