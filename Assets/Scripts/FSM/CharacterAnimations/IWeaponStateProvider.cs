namespace FSM.CharacterAnimations
{
    public interface IWeaponStateProvider
    {
        WeaponType CurrentWeapon { get; }
    }
}