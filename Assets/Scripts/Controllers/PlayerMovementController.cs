using CharacterInput;
using UnityEngine;
using Zenject;

namespace Controllers
{
    public sealed class PlayerMovementController : ITickable
    {
        private const float MoveSpeed = 4f;
        private const float SprintSpeed = 8f;
        
        private readonly CharacterController _characterController;
        private  readonly IMoveInputHandler _input;

        private Vector2 _moveInput;
        private Transform _moveTransform;
        private Vector3 _moveDirection;

        public PlayerMovementController(CharacterController characterController, IMoveInputHandler input)
        {
            _characterController = characterController;
            _input = input;
            _moveTransform = characterController.transform;
        }

         void ITickable.Tick() => Move(_input.MoveInput);


         private void Move(Vector2 direction)
        {
            if (direction.sqrMagnitude < 0.01f )
                return;

            _moveDirection.Set(direction.x, 0f, direction.y);
            var worldDirection = _moveTransform.TransformDirection(_moveDirection);
            var distance = worldDirection.magnitude;

            // движение
            var speed = _input.IsSprinting ? SprintSpeed : MoveSpeed;
            _characterController.Move(worldDirection * (speed * Time.deltaTime));

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