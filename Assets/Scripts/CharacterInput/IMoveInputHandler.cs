using UnityEngine;

namespace CharacterInput
{
    public interface IMoveInputHandler
    {
        Vector2 MoveInput { get; }
        bool IsSprinting { get; }
    }
}