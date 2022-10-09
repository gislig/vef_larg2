using Battleground.Models.InputModels;
using GraphQL.Types;
using Npgsql.PostgresTypes;

namespace Battleground.Api.Schema.InputTypes;

public class AttackInputType : InputObjectGraphType<AttackInputModel>
{
    public AttackInputType()
    {
        Name = "AttackInput";
        Field(x => x.AttackerId);
        Field(x => x.PokemonIdentifier);
        Field(x => x.BattleId);
    }
}