using Unity.Mathematics;

namespace Input
{
    public interface IMoveInputHandler
    {
        float2 MoveInput { get; }
        bool IsSprinting { get; }
    }
}