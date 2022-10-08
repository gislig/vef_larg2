using Battleground.Models.InputModels;
using GraphQL.Types;

namespace Battleground.Api.Schema.InputTypes;

public class AttackInputType : InputObjectGraphType<AttackInputModel>
{
    public AttackInputType()
    {
        Name = "AttackInput";    
    }
}