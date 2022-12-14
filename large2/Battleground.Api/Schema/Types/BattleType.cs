using Battleground.Models.Dtos;
using GraphQL.Types;
using Npgsql.PostgresTypes;

namespace Battleground.Api.Schema.Types
{
    public class BattleType : ObjectGraphType<BattleDto>
    {
        public BattleType()
        {
            Field(x => x.Id).Description("The id of the Battle.");
            Field(x => x.Winner, type: typeof(PlayerType), nullable: true).Description("Battle Winner");
            Field(x => x.WinnerId).Description("The Id of the winner for this battle");
            Field(x => x.StatusId).Description("The Status of the battle");
            Field(x => x.PlayersInMatch, type: typeof(ListGraphType<PlayerType>), nullable: true).Description("Players in match");
            Field(x => x.BattlePokemons, type: typeof(ListGraphType<PokemonType>), nullable: true).Description("Pokemons in battle");
            Field(x => x.Attacks, type: typeof(ListGraphType<AttackType2>), nullable: true).Description("attacks in match");
            // Field(x => x.Attacks, type: typeof(AttackDto)).Description("Attacks in the mathc");
        }
    }
}
// ■status: BattleStatus*
// ■winner: PlayerType
// ■battlePokemons: An array of PokemonType (where the array cannot
// be null nor the items within the array)
// ■playersInMatch: An array of PlayerType (where the array cannot be
// null nor the items within the array)
// ■attacks: An array of AttackType (where the array cannot be null nor
// the items within the array)

            // Field(x => x.Status).Description("Status of the battle.");

            // TODO: Solve fields below.

            // Field(x => x.).Description(""); // winner: PlayerType
            // Field<PlayerType>("winner")
            //     .Resolve(context => {
            //         return null;
            //     });

            // // Field(x => x.).Description(""); // battlePokemons: An array of PokemonType (where the array cannot be null nor the items within the array)
            // Field<NonNullGraphType<ListGraphType<NonNullGraphType<PokemonType>>>>("battlePokemons")
            //     .Resolve(context => {
            //         return null;
            //     });

            // // Field(x => x.).Description(""); // playersInMatch: An array of PlayerType (where the array cannot be null nor the items within the array)
            // Field<NonNullGraphType<ListGraphType<NonNullGraphType<PlayerType>>>>("playerInMatch")
            //     .Resolve(context => {
            //         return null;
            //     });

            // // Field(x => x.).Description(""); // ■ attacks: An array of AttackType (where the array cannot be null nor the items within the array)
            // Field<NonNullGraphType<ListGraphType<NonNullGraphType<AttackType>>>>("attacks")
            //     .Resolve(context => {
            //         return null;
            //     });
//         }
//     }
// }
