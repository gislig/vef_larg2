using Battleground.Models.Dtos;
using Battleground.Repositories.Entities;
using GraphQL.Types;

namespace Battleground.Api.Schema.Types
{
    public class AttackType2 : ObjectGraphType<AttackTypeDto>
    {
        public AttackType2()
        {
            //TODO Need to rename fields
            Field(x => x.DamageDealt).Description("The damage of the attack");
            Field(x => x.CriticalHit).Description("Critical hit chance");
            Field(x => x.SuccessHit).Description("If the attack was successful");
            Field(x => x.AttackedBy, type: typeof(PokemonType), nullable: true).Description("Pokemon that made the attack");
            // Field(x => x.PokemonIdentifier).Description("The name of the pokemon");
            // Field(x => x.BattlePokemonId).Description("The id of the pokemon in battle");
            // Field(x => x.BattleId).Description("The id of the battle");
            // Field(x => x.AttackerId).Description("The id of the attacker");
    
        }
    }
}