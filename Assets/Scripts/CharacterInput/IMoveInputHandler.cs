using R3;
using UnityEngine;

namespace CharacterInput
{
    public interface IMoveInputHandler
    {
        Observable<Vector2> MoveStream { get; }
        Observable<bool> SprintStream { get; }
    }

    public interface ILookInputHandler
    {
        Observable<Vector2> LookStream { get; }
    }

    public enum AttackType
    {
        Punch,
        Hook,
        Shoot
    }
    public interface IAttackInputHandler
    {
        Observable<AttackType> AttackStream { get; }
        Observable<bool> MeleeModeStream { get; }
    }
}