using System;
using UnityEngine.InputSystem;
using Zenject;

namespace Input
{
    public sealed class AttackInputAction : IInitializable, ITickable, IDisposable ,IAttackInputHandler
    {
        public bool IsLightAttacking { get; private set; }
    
        public bool IsHeavyAttacking { get; private set; }

        private InputAction _lightAttack;
        private InputAction _heavyAttack;

        void IInitializable.Initialize()
        {
            _lightAttack = new InputAction("LightAttack", binding: "<Mouse>/leftButton");
            _lightAttack.AddBinding("<Gamepad>/rightTrigger");

            _heavyAttack = new InputAction("HeavyAttack", binding: "<Mouse>/rightButton");
            _heavyAttack.AddBinding("<Gamepad>/leftTrigger");

            _lightAttack.Enable();
            _heavyAttack.Enable();
        }
     
        void ITickable.Tick()
        {
            IsLightAttacking = _lightAttack.IsPressed();
            IsHeavyAttacking = _heavyAttack.IsPressed();
        }
     
        void IDisposable.Dispose()
        {
            _lightAttack?.Dispose();
            _heavyAttack?.Dispose();
        }
    }
}