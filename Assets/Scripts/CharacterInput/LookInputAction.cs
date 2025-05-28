using System;
using R3;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace CharacterInput
{
    public sealed class LookInputAction : IInitializable, IDisposable, ILookInputHandler
    {
        public Observable<Vector2> LookStream => _lookSubject.DistinctUntilChanged();
        
        private readonly Subject<Vector2> _lookSubject = new();
        
        private readonly InputAction _look;

        public LookInputAction()
        {
            _look = new InputAction("Look");
            _look.AddBinding("<Mouse>/delta");
            _look.AddBinding("<Gamepad>/rightStick");

            _look.performed += ctx => _lookSubject.OnNext(ctx.ReadValue<Vector2>());
            _look.canceled  += _   => _lookSubject.OnNext(Vector2.zero);
        }
        
         void IInitializable.Initialize() => _look.Enable();

         void IDisposable.Dispose()
        {
            _look.Disable();
            _look.Dispose();
            _lookSubject.Dispose();
        }
    }
}