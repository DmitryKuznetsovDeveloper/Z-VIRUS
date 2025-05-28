using System;
using CharacterInput;
using R3;
using UnityEngine;
using Zenject;

namespace Core
{
    public sealed class PlayerLookController :IInitializable, IDisposable
    {
        private const float RotationSpeed = 180f;

        private readonly ILookInputHandler _look;
        private readonly Transform _transform;
        
        private IDisposable _lookSub;
        private IDisposable _tickSub;

        private float _angle;
        private float _lookInputX;

        public PlayerLookController(ILookInputHandler look, Transform transform)
        {
            _look = look;
            _transform = transform;
        }
        
        void IInitializable.Initialize()
        {
            _lookSub = _look.LookStream
                .Subscribe(look => _lookInputX = look.x);

            _tickSub = Observable.EveryUpdate()
                .Subscribe(_ => Rotate());
        }

        void IDisposable.Dispose()
        {
            _lookSub?.Dispose();
            _tickSub?.Dispose();
        }
        
        private void Rotate()
        {
            if (Mathf.Abs(_lookInputX) < 0.01f)
                return;

            _angle += _lookInputX * RotationSpeed * Time.deltaTime;
            _transform.rotation = Quaternion.Euler(0f, _angle, 0f);
        }
    }
}