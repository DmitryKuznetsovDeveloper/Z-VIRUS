using R3;
using UnityEngine;

namespace CharacterInput
{
    public interface ICharacterInputHandler
    {
        ReadOnlyReactiveProperty<Vector2> MoveStream { get; }
        ReadOnlyReactiveProperty<Vector2> LookStream { get; }
        ReadOnlyReactiveProperty<float> SprintStream { get; }
        ReadOnlyReactiveProperty<bool> ShootStream { get; }
    }
}