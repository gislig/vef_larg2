using Battleground.Models.Dtos;
using Battleground.Repositories.Entities;
using GraphQL.Types;

namespace Battleground.Api.Schema.Types
{
    public class AttackType : ObjectGraphType<AttackDto>
    {
        public AttackType()
        {
            //TODO Need to rename fields
            Field(x => x.Damage).Description("The damage of the attack");
            Field(x => x.CriticalHit).Description("Critical hit chance");
            Field(x => x.Success).Description("If the attack was successful");
            Field(x => x.PokemonIdentifier).Description("The name of the pokemon");
            Field(x => x.BattlePokemonId).Description("The id of the pokemon in battle");
            Field(x => x.BattleId).Description("The id of the battle");
            Field(x => x.AttackerId).Description("The id of the attacker");
            
            // TODO: Field -> attackedBy: PokemonType
            // Field<ObjectGraphType<PokemonType>>("attackedBy")
            //     .Resolve(context => {
            //         return null;
            //     });
        }
    }
}