using Animancer;
using FSM;
using CharacterInput;
using Data;
using FSM.CharacterAnimations;
using Zenject;

public sealed class CharacterAnimationStateMachine : ITickable
{
    private readonly StateMachine _stateMachine = new();

    private readonly AnimancerComponent _animancer;
    private readonly IWeaponStateProvider _weapon;
    private readonly IMoveInputHandler _moveInput;
    private readonly IAttackInputHandler _attackInput;
    private readonly WeaponAnimationCollection _collection;
    private readonly MeleeComboAnimationSet _meleeComboSet;

    private AttackComboState _meleeComboState;
    private IdleState _meleeIdleState;
    private ShootState _shootState;
    private RunState _runState;
    private WalkState _walkState;
    private IdleState _idleState;

    private WeaponType _cachedWeapon;

    public CharacterAnimationStateMachine(
        AnimancerComponent animancer,
        IWeaponStateProvider weapon,
        IMoveInputHandler moveInput,
        IAttackInputHandler attackInput,
        WeaponAnimationCollection collection,
        MeleeComboAnimationSet meleeComboSet)
    {
        _animancer = animancer;
        _weapon = weapon;
        _moveInput = moveInput;
        _attackInput = attackInput;
        _collection = collection;
        _meleeComboSet = meleeComboSet;

        SetupStatesForWeapon(_weapon.CurrentWeapon);
    }

    public void Tick()
    {
        _stateMachine.Tick();

        if (_cachedWeapon != _weapon.CurrentWeapon)
        {
            SetupStatesForWeapon(_weapon.CurrentWeapon);
        }

        if (_weapon.CurrentWeapon == WeaponType.Unarmed)
        {
            TrySet(_meleeComboState);
            TrySet(_meleeIdleState);
        }
        else
        {
            TrySet(_shootState);
            TrySet(_runState);
            TrySet(_walkState);
            TrySet(_idleState);
        }
    }

    private void SetupStatesForWeapon(WeaponType weapon)
    {
        _cachedWeapon = weapon;

        if (weapon == WeaponType.Unarmed)
        {
            _meleeComboState = new AttackComboState(_animancer, _attackInput, _meleeComboSet);
            _meleeIdleState = new IdleState(_animancer, _meleeComboSet.Idle.Clip, _moveInput);
        }
        else
        {
            var currentSet = _collection.GetSet(weapon);
            _shootState = new ShootState(_animancer, currentSet.Shoot, _attackInput, currentSet.UpperBodyMask);
            _runState = new RunState(_animancer, currentSet.Run, _moveInput);
            _walkState = new WalkState(_animancer, currentSet.Walk, _moveInput);
            _idleState = new IdleState(_animancer, currentSet.Idle, _moveInput);
        }
    }

    private void TrySet(object state) => _stateMachine.SetState(state);
}
