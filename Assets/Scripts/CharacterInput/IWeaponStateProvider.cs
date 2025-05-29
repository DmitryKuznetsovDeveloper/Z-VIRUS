using R3;
using Service;

namespace CharacterInput
{
    public interface IWeaponStateProvider
    {
        Observable<WeaponType> WeaponStream { get; }
    }
}