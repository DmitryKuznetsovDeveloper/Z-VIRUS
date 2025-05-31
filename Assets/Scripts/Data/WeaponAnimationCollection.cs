using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Configs/Animations/WeaponAnimationCollection")]
    public sealed class WeaponAnimationCollection : ScriptableObject
    {
        public WeaponAnimationSet Unarmed;
        public WeaponAnimationSet Pistol;
        public WeaponAnimationSet Rifle;

        public WeaponAnimationSet GetSet(WeaponType weapon)
        {
            return weapon switch
            {
                WeaponType.Unarmed => Unarmed,
                WeaponType.Pistol => Pistol,
                WeaponType.Rifle => Rifle,
            };
        }
    }
}