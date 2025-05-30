using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Animations/WeaponAnimationCollection")]
public sealed class WeaponAnimationCollection : ScriptableObject
{
    public WeaponAnimationSet Pistol;
    public WeaponAnimationSet Rifle;

    public WeaponAnimationSet GetSet(WeaponType weapon)
    {
        return weapon switch
        {
            WeaponType.Pistol => Pistol,
            WeaponType.Rifle => Rifle,
        };
    }
}