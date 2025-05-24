using R3;
using UnityEngine;

namespace Input
{
    public interface ICharacterInputHandler
    {
        ReadOnlyReactiveProperty<Vector2> MoveStream { get; }
        ReadOnlyReactiveProperty<Vector2> LookStream { get; }
        ReadOnlyReactiveProperty<float> SprintStream { get; }
        ReadOnlyReactiveProperty<bool> ShootStream { get; }
    }
}