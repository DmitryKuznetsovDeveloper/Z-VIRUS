using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace CharacterInput
{
    public sealed class LookInputAction : IInitializable,ITickable, IDisposable, ILookInputHandler
    {
        public Vector2 LookInput { get; private set; } = Vector2.zero;
        
        private readonly InputAction _look;

        public LookInputAction()
        {
            _look = new InputAction("Look");
            _look.AddBinding("<Mouse>/delta");
            _look.AddBinding("<Gamepad>/rightStick");
        }
        
         void IInitializable.Initialize() => _look.Enable();
         
         void ITickable.Tick() => LookInput = _look.ReadValue<Vector2>();

         void IDisposable.Dispose()
        {
            _look.Disable();
            _look.Dispose();
        }
    }
}