using Battleground.Models.InputModels;
using GraphQL.Types;

namespace Battleground.Api.Schema.InputTypes;

public class PlayerInputType : InputObjectGraphType<PlayerInputModel>
{
    public PlayerInputType()
    {
        Name = "PlayerInputType";
        Field(b => b.Name).Description("The name of the player");
    }
}