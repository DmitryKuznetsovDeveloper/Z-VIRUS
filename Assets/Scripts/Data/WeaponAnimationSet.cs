using Animancer;
using Data;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Animations/WeaponAnimationSet")]
public sealed class WeaponAnimationSet : ScriptableObject
{
    public AnimationClip Idle;
    public MoveAnimationConfig Walk;
    public MoveAnimationConfig Run;
    public ClipTransition Shoot;
    public AvatarMask UpperBodyMask;
}
public enum WeaponType
{
    Unarmed,
    Pistol,
    Rifle
}