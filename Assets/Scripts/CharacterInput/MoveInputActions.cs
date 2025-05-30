using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace CharacterInput
{

    public sealed class MoveInputActions : IInitializable, ITickable, IDisposable, IMoveInputHandler
    {
        public Vector2 MoveInput { get; private set; } = Vector2.zero;
        public bool IsSprinting { get; private set; } 

        private readonly InputAction _move;
        private readonly InputAction _sprint;

        public MoveInputActions()
        {
            _move = new InputAction("Move");
            _move.AddCompositeBinding("2DVector")
                .With("Up", "<Keyboard>/w")
                .With("Down", "<Keyboard>/s")
                .With("Left", "<Keyboard>/a")
                .With("Right", "<Keyboard>/d");
            _move.AddBinding("<Gamepad>/leftStick");

            _sprint = new InputAction("Sprint");
            _sprint.AddBinding("<Keyboard>/leftShift");
            _sprint.AddBinding("<Gamepad>/leftStickPress");
        }

         void IInitializable.Initialize()
        {
            _move.Enable();
            _sprint.Enable();
        }
         
         void ITickable.Tick()
        {
            MoveInput = _move.ReadValue<Vector2>();
            IsSprinting = _sprint.IsPressed();
        }
         
        void IDisposable.Dispose()
        {
            _move.Disable();
            _sprint.Disable();
            _move.Dispose();
            _sprint.Dispose();
        }
    }
}