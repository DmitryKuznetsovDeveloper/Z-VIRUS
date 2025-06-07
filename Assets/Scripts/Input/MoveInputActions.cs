using System;
using Unity.Mathematics;
using UnityEngine.InputSystem;
using Zenject;

namespace Input
{

    public sealed class MoveInputActions : IInitializable, ITickable, IDisposable, IMoveInputHandler
    {
        public float2 MoveInput { get; private set; }  = float2.zero;
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
            MoveInput = _move.ReadValue<float2>();
            IsSprinting = _sprint.IsPressed();
        }
         
        void IDisposable.Dispose()
        {
            _move.Dispose();
            _sprint.Dispose();
        }
    }
}