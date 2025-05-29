using System;
using R3;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace CharacterInput
{
    public sealed class MoveInputActions : IInitializable, IDisposable, IMoveInputHandler
    {
        public Observable<Vector2> MoveStream => _moveSubject.DistinctUntilChanged();
        public Observable<bool> SprintStream => _sprintSubject.DistinctUntilChanged();
        
        private readonly Subject<Vector2> _moveSubject = new();
        private readonly Subject<bool> _sprintSubject = new();
        
        private readonly InputAction _move;
        private readonly InputAction _sprint;

        public MoveInputActions()
        {
            // === MOVE ===
            _move = new InputAction("Move");
            _move.AddCompositeBinding("2DVector")
                .With("Up", "<Keyboard>/w")
                .With("Down", "<Keyboard>/s")
                .With("Left", "<Keyboard>/a")
                .With("Right", "<Keyboard>/d");
            _move.AddBinding("<Gamepad>/leftStick");

            _move.performed += ctx => _moveSubject.OnNext(ctx.ReadValue<Vector2>());
            _move.canceled  += _   => _moveSubject.OnNext(Vector2.zero);
            
            // === SPRINT ===
            _sprint = new InputAction("Sprint");
            _sprint.AddBinding("<Keyboard>/leftShift");
            _sprint.AddBinding("<Gamepad>/leftStickPress");

            _sprint.performed += ctx => _sprintSubject.OnNext(true);
            _sprint.canceled  += _   => _sprintSubject.OnNext(false);
        }

        public void Initialize()
        {
            _move.Enable();
            _sprint.Enable();
            _moveSubject.OnNext(Vector2.zero);
        }

        public void Dispose()
        {
            _move.Disable();
            _sprint.Disable();
            _move.Dispose();
            _sprint.Dispose();
            _moveSubject.Dispose();
            _sprintSubject.Dispose();
        }
    }
}
