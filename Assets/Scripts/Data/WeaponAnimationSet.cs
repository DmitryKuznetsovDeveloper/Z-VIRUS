using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "WeaponAnimationSet", menuName = "Configs/Weapon Animation Set")]
    public sealed class WeaponAnimationSet : ScriptableObject
    {
        public AnimationClip Idle;
        public MoveAnimationConfig Walk;
        public MoveAnimationConfig Run;
        public AnimationClip[] Attacks;
        public AvatarMask UpperBodyMask;
        public float ComboResetTime = 1f;
    }

    public enum WeaponType
    {
        Unarmed,
        Pistol,
        Rifle
    }
}