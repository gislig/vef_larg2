using Battleground.Api.Schema.Queries;
using GraphQL.Instrumentation;

namespace Battleground.Api.Schema;

public class BattlegroundSchema : GraphQL.Types.Schema
{
    public BattlegroundSchema(IServiceProvider provider)
        : base(provider)
    {
        Query = (BattlegroundQuery)provider.GetService(typeof(BattlegroundQuery))! ?? throw new InvalidOperationException();
        FieldMiddleware.Use(new InstrumentFieldsMiddleware());
    }
}
