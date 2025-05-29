using System;
using R3;
using Service;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace CharacterInput
{
    public sealed class WeaponSelectorAction : IInitializable, IDisposable, IWeaponStateProvider
    {
        private readonly BehaviorSubject<WeaponType> _weaponSubject = new(WeaponType.Pistol);
        public Observable<WeaponType> WeaponStream => _weaponSubject;

        private InputAction _select1;
        private InputAction _select2;
        private InputAction _scroll;

        private readonly WeaponType[] _weaponOrder = { WeaponType.Pistol, WeaponType.Rifle };
        private int _currentIndex = 0;

        public void Initialize()
        {
            // Цифровые кнопки
            _select1 = new InputAction(binding: "<Keyboard>/1");
            _select2 = new InputAction(binding: "<Keyboard>/2");

            _select1.performed += _ => SetWeaponByIndex(0);
            _select2.performed += _ => SetWeaponByIndex(1);

            _select1.Enable();
            _select2.Enable();

            // Колесико мыши
            _scroll = new InputAction(binding: "<Mouse>/scroll");
            _scroll.performed += ctx =>
            {
                var scroll = ctx.ReadValue<Vector2>().y;
                if (scroll > 0.01f)
                    ScrollWeapon(-1); // вверх
                else if (scroll < -0.01f)
                    ScrollWeapon(1);  // вниз
            };
            _scroll.Enable();

            _weaponSubject.OnNext(_weaponOrder[_currentIndex]);
        }

        public void Dispose()
        {
            _select1?.Dispose();
            _select2?.Dispose();
            _scroll?.Dispose();
            _weaponSubject?.Dispose();
        }

        private void SetWeaponByIndex(int index)
        {
            if (index < 0 || index >= _weaponOrder.Length) return;
            _currentIndex = index;
            _weaponSubject.OnNext(_weaponOrder[_currentIndex]);
        }

        private void ScrollWeapon(int direction)
        {
            _currentIndex += direction;
            if (_currentIndex < 0) _currentIndex = _weaponOrder.Length - 1;
            if (_currentIndex >= _weaponOrder.Length) _currentIndex = 0;

            _weaponSubject.OnNext(_weaponOrder[_currentIndex]);
        }
    }
}
