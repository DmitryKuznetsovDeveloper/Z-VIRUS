using System;
using UnityEngine.InputSystem;
using Unity.Mathematics;
using R3;
using UnityEngine;
using Zenject;

namespace Input
{
    public sealed class CharacterInputActions : IInitializable, IDisposable, ICharacterInputHandler
    {
        public ReadOnlyReactiveProperty<Vector2> MoveStream => _moveStream;
        public ReadOnlyReactiveProperty<Vector2> LookStream => _lookStream;
        public ReadOnlyReactiveProperty<float> SprintStream => _sprintStream;
        public ReadOnlyReactiveProperty<bool> ShootStream => _shootStream;

        private readonly ReactiveProperty<Vector2> _moveStream = new();
        private readonly ReactiveProperty<Vector2> _lookStream = new();
        private readonly ReactiveProperty<float> _sprintStream = new();
        private readonly ReactiveProperty<bool> _shootStream = new();

        private readonly InputAction _move;
        private readonly InputAction _look;
        private readonly InputAction _sprint;
        private readonly InputAction _shoot;

        public CharacterInputActions()
        {
            // === MOVE ===
            _move = new InputAction("Move");
            _move.AddCompositeBinding("2DVector")
                .With("Up", "<Keyboard>/w")
                .With("Down", "<Keyboard>/s")
                .With("Left", "<Keyboard>/a")
                .With("Right", "<Keyboard>/d");
            _move.AddBinding("<Gamepad>/leftStick");
            _move.performed += ctx => _moveStream.Value = ctx.ReadValue<Vector2>();
            _move.canceled += _ => _moveStream.Value = float2.zero;

            // === LOOK ===
            _look = new InputAction("Look");
            _look.AddBinding("<Mouse>/delta");
            _look.AddBinding("<Gamepad>/rightStick");
            _look.performed += ctx => _lookStream.Value = ctx.ReadValue<Vector2>();
            _look.canceled += _ => _lookStream.Value = float2.zero;

            // === SPRINT ===
            _sprint = new InputAction("Sprint");
            _sprint.AddBinding("<Keyboard>/leftShift");
            _sprint.AddBinding("<Gamepad>/leftStickPress");
            _sprint.performed += ctx => _sprintStream.Value = ctx.ReadValue<float>();
            _sprint.canceled += _ => _sprintStream.Value = 0f;

            // === SHOOT ===
            _shoot = new InputAction("Shoot", InputActionType.Button);
            _shoot.AddBinding("<Mouse>/leftButton");
            _shoot.AddBinding("<Gamepad>/rightTrigger");
            _shoot.performed += _ => _shootStream.Value = true;
            _shoot.canceled += _ => _shootStream.Value = false;
        }

        public void Disable()
        {
            _move.Disable();
            _look.Disable();
            _sprint.Disable();
            _shoot.Disable();
        }

        public void Initialize()
        {
            _move.Enable();
            _look.Enable();
            _sprint.Enable();
            _shoot.Enable();
        }

        public void Dispose()
        {
            Disable();
            _move.Dispose();
            _look.Dispose();
            _sprint.Dispose();
            _shoot.Dispose();
            _moveStream.Dispose();
            _lookStream.Dispose();
            _sprintStream.Dispose();
            _shootStream.Dispose();
        }
    }
}