using Animancer;
using CharacterInput;
using Data;
using Zenject;

namespace FSM.CharacterAnimations
{
    public sealed class CharacterAnimationStateMachine : ITickable
    {
        private readonly StateMachine _stateMachine = new();

        private readonly AnimancerComponent _animancer;
        private readonly IWeaponStateProvider _weapon;
        private readonly IMoveInputHandler _moveInput;
        private readonly IAttackInputHandler _attackInput;
        private readonly WeaponAnimationCollection _collection;

        // Все состояния реализуют FSM.IState
        private  AttackState _attackState;
        private RunState _runState;
        private WalkState _walkState;
        private IdleState _idleState;

        private WeaponType _cachedWeapon;

        public CharacterAnimationStateMachine(
            AnimancerComponent animancer,
            IWeaponStateProvider weapon,
            IMoveInputHandler moveInput,
            IAttackInputHandler attackInput,
            WeaponAnimationCollection collection)
        {
            _animancer = animancer;
            _weapon = weapon;
            _moveInput = moveInput;
            _attackInput = attackInput;
            _collection = collection;

            SetupStatesForWeapon(_weapon.CurrentWeapon);
        }

        public void Tick()
        {
            _stateMachine.Tick();

            if (_cachedWeapon != _weapon.CurrentWeapon)
            {
                SetupStatesForWeapon(_weapon.CurrentWeapon);
            }

            if (_attackState is AttackState attack && attack.IsPlaying)
            {
                _stateMachine.TrySet(_attackState);
            }
            else
            {
                _stateMachine.TrySetMany(
                    _attackState,
                    _runState,
                    _walkState,
                    _idleState
                );
            }
        }

        private void SetupStatesForWeapon(WeaponType weapon)
        {
            _cachedWeapon = weapon;

            var currentSet = _collection.GetSet(weapon);

            _attackState = new AttackState(
                _animancer,
                _attackInput,
                currentSet.Attacks,
                currentSet.ComboResetTime
            );

            _runState = new RunState(_animancer, currentSet.Run, _moveInput);
            _walkState = new WalkState(_animancer, currentSet.Walk, _moveInput);
            _idleState = new IdleState(_animancer, currentSet.Idle, _moveInput);
        }
    }
}
