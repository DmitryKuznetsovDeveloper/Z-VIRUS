using System;
using CharacterInput;
using R3;
using UnityEngine.InputSystem;
using Zenject;

public sealed class AttackInputAction : IInitializable, IDisposable, IAttackInputHandler
{
    public Observable<AttackType> AttackStream => _attackSubject;
    public Observable<bool> MeleeModeStream => _meleeModeSubject;

    private readonly Subject<AttackType> _attackSubject = new();
    private readonly Subject<bool> _meleeModeSubject = new();

    private InputAction _attack;
    private InputAction _combatHold;
    private bool _currentCombatState = false;

    public void Initialize()
    {
        _combatHold = new InputAction(binding: "<Mouse>/rightButton");
        _combatHold.AddBinding("<Gamepad>/leftTrigger");

        _combatHold.performed += _ =>
        {
            _currentCombatState = true;
            _meleeModeSubject.OnNext(true);
        };

        _combatHold.canceled += _ =>
        {
            _currentCombatState = false;
            _meleeModeSubject.OnNext(false);
        };

        _attack = new InputAction(binding: "<Mouse>/leftButton");
        _attack.AddBinding("<Gamepad>/rightTrigger");
        
        _attack.performed += _ =>
        {
            if (!_currentCombatState) return;

            var attackType = UnityEngine.Random.value > 0.5f ? AttackType.Punch : AttackType.Hook;
            _attackSubject.OnNext(attackType);
        };

        _attack.Enable();
        _combatHold.Enable();
    }

    public void Dispose()
    {
        _attack?.Dispose();
        _combatHold?.Dispose();
        _attackSubject?.Dispose();
        _meleeModeSubject?.Dispose();
    }
}