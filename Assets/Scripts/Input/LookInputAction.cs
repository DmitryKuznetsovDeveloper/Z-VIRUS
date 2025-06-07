using System;
using Unity.Mathematics;
using UnityEngine.InputSystem;
using Zenject;

namespace Input
{
    public sealed class LookInputAction : IInitializable,ITickable, IDisposable, ILookInputHandler
    {
        public float2 LookInput { get; private set; } = float2.zero;
        
        private readonly InputAction _look;

        public LookInputAction()
        {
            _look = new InputAction("Look");
            _look.AddBinding("<Mouse>/delta");
            _look.AddBinding("<Gamepad>/rightStick");
        }
        
         void IInitializable.Initialize() => _look.Enable();
         
         void ITickable.Tick() => LookInput = _look.ReadValue<float2>();

         void IDisposable.Dispose()
        {
            _look.Disable();
            _look.Dispose();
        }
    }
}