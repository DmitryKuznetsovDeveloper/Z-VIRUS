using UnityEngine;
using UnityEngine.InputSystem;
using FSM.CharacterAnimations;
using Zenject;

public sealed class WeaponSelectorAction : IWeaponStateProvider, IInitializable, ITickable
{
    public WeaponType CurrentWeapon => _weaponOrder[_currentIndex];

    private int _currentIndex;
    private readonly WeaponType[] _weaponOrder = new[]
    {
        WeaponType.Unarmed,
        WeaponType.Pistol,
        WeaponType.Rifle
    };

    private InputAction _select1;
    private InputAction _select2;
    private InputAction _select3;
    private InputAction _scroll;

     void IInitializable.Initialize()
    {
        _select1 = new InputAction(binding: "<Keyboard>/1");
        _select2 = new InputAction(binding: "<Keyboard>/2");
        _select3 = new InputAction(binding: "<Keyboard>/3");
        _scroll = new InputAction(binding: "<Mouse>/scroll");

        _select1.Enable();
        _select2.Enable();
        _select3.Enable();
        _scroll.Enable();
    }

     void ITickable.Tick()
    {
        if (_select1.WasPerformedThisFrame()) _currentIndex = 0;
        else if (_select2.WasPerformedThisFrame()) _currentIndex = 1;
        else if (_select3.WasPerformedThisFrame()) _currentIndex = 2;

        if (!_scroll.WasPerformedThisFrame()) 
            return;
        
        var scroll = _scroll.ReadValue<Vector2>().y;
        _currentIndex = scroll switch
        {
            > 0.01f => (_currentIndex + 1) % _weaponOrder.Length,
            < -0.01f => (_currentIndex - 1 + _weaponOrder.Length) % _weaponOrder.Length,
            _ => _currentIndex
        };
    }
}