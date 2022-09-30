using Battleground.Api.Schema.Types;
using GraphQL.Types;

namespace Battleground.Api.Schema.Mutations
{
    public class BattlegroundMutation : ObjectGraphType
    {
        public BattlegroundMutation()
        {
            Field<BooleanGraphType>("removePlayer")
                .Argument<NonNullGraphType<IntGraphType>>("id")
                .Resolve(context => {
                    return null;
                });
        }        
    }
}