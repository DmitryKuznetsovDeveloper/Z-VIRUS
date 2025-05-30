using System;
using Animations.Character;
using CharacterInput;
using R3;
using UnityEngine;
using Zenject;

namespace Controllers
{
    public sealed class PlayerMovementController : IInitializable, IDisposable
    {
        private const float MoveSpeed = 4f;
        private const float SprintSpeed = 8f;
        
        private readonly CharacterController _characterController;
        private readonly MoveAnimatorController _moveAnimator;
        private  readonly IMoveInputHandler _input;
        private  readonly IAttackInputHandler _attackInput;

        private Vector2 _moveInput;
        private Transform _moveTransform;
        private Vector3 _moveDirection;
        private bool _isSprinting;
        private bool _isMeleeAttacking;
        
        private IDisposable _sprintSub;
        private IDisposable _moveSub;
        private IDisposable _tickSub;
        private IDisposable _meleeModeSub;

        public PlayerMovementController(CharacterController characterController, MoveAnimatorController moveAnimator, IMoveInputHandler input, IAttackInputHandler attackInput)
        {
            _characterController = characterController;
            _moveAnimator = moveAnimator;
            _input = input;
            _attackInput = attackInput;
            _moveTransform = characterController.transform;
        }

         void IInitializable.Initialize()
        {
            _meleeModeSub = _attackInput.MeleeModeStream.Subscribe(isMeleeAttacking => _isMeleeAttacking = isMeleeAttacking);
            _moveSub = _input.MoveStream.Subscribe(input => _moveInput = input);
            _sprintSub = _input.SprintStream.Subscribe(isSprinting => _isSprinting = isSprinting);
            _tickSub = Observable.EveryUpdate().Subscribe(_ =>
            {
                Move(_moveInput);
                _moveAnimator.OnMoveAnimation(_moveInput);
            });

        }

        void IDisposable.Dispose()
        {
            _moveSub?.Dispose();
            _sprintSub?.Dispose();
            _tickSub?.Dispose();
        }
        
        private void Move(Vector2 direction)
        {
            if (direction.sqrMagnitude < 0.01f || _isMeleeAttacking)
            {
                return;
            }

            _moveDirection.Set(direction.x, 0f, direction.y);
            var worldDirection = _moveTransform.TransformDirection(_moveDirection);
            var distance = worldDirection.magnitude;

            // движение
            var speed = _isSprinting ? SprintSpeed : MoveSpeed;
            _characterController.Move(worldDirection * (speed * Time.deltaTime));

            // синхронизация скорости движения с анимацией
            _moveAnimator.SetAnimationSpeed(distance);

            // поворот (если есть боковое направление)
            if (!(Mathf.Abs(direction.x) > 0.01f)) return;
            var lookDir = new Vector3(direction.x, 0, 0);
            lookDir = _moveTransform.TransformDirection(lookDir);
            var targetRotation = Quaternion.LookRotation(lookDir);
            _moveTransform.rotation = Quaternion.RotateTowards(
                _moveTransform.rotation,
                targetRotation,
                90f * Time.deltaTime
            );
        }
    }
}