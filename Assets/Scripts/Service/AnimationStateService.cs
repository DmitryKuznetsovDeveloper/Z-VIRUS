using System;
using Animations.Character;
using CharacterInput;
using Data;
using R3;
using Zenject;

namespace Service
{
    public enum MovementState
    {
        Walk,
        Sprint
    }
    
    public enum WeaponType
    {
        None,
        Pistol,
        Rifle
    }

    public sealed class AnimationStateService : IInitializable, IDisposable
    {
        private readonly MoveAnimatorController _moveAnimator;
        private readonly MoveAnimationLibrary _library;
        private readonly IMoveInputHandler _input;
        private readonly IWeaponStateProvider _weaponStateProvider; // твой интерфейс оружия

        private IDisposable _sprintSub;
        private IDisposable _weaponSub;

        private MovementState _currentMoveState = MovementState.Walk;
        private WeaponType _currentWeapon = WeaponType.None;

        public AnimationStateService(MoveAnimatorController animator, MoveAnimationLibrary library, IMoveInputHandler input, IWeaponStateProvider weaponStateProvider)
        {
            _moveAnimator = animator;
            _library = library;
            _input = input;
            _weaponStateProvider = weaponStateProvider;
        }

        public void Initialize()
        {
            _sprintSub = _input.SprintStream.Subscribe(isSprinting  =>
            {
                UnityEngine.Debug.Log($"[SprintStream] Shift pressed: {isSprinting}");
                _currentMoveState = isSprinting ? MovementState.Sprint : MovementState.Walk;
                UpdateConfig();
            });

            _weaponSub = _weaponStateProvider.WeaponStream.Subscribe(weapon =>
            {
                _currentWeapon = weapon;
                UpdateConfig();
            });
        }

        private void UpdateConfig()
        {
            var config = _library.GetByState(_currentMoveState, _currentWeapon);
            UnityEngine.Debug.Log($"[AnimationStateService] Set config: {_currentMoveState} + {_currentWeapon} => {config?.name}");
            _moveAnimator.SetAnimationConfig(config);
        }

        public void Dispose()
        {
            _sprintSub?.Dispose();
            _weaponSub?.Dispose();
        }
    }
}