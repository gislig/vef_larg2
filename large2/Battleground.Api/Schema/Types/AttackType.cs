using Battleground.Repositories.Entities;
using GraphQL.Types;

namespace Battleground.Api.Schema.Types
{
    public class AttackType : ObjectGraphType<Attack>
    {
        public AttackType()
        {
            Field(x => x.Damage).Description("");
            Field(x => x.CriticalHit).Description("");
            Field(x => x.Success).Description("");
            // TODO: Field -> attackedBy: PokemonType
            Field<ObjectGraphType<PokemonType>>("attackedBy")
                .Resolve(context => {
                    return null;
                });
        }
    }
}