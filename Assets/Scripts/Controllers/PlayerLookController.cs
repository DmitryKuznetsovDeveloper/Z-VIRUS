using CharacterInput;
using UnityEngine;
using Zenject;

namespace Controllers
{
    public sealed class PlayerLookController :ITickable
    {
        private const float RotationSpeed = 180f;
        private readonly ILookInputHandler _look;
        private readonly Transform _transform;
        private float _angle;
        private float _lookInputX;

        public PlayerLookController(ILookInputHandler look, Transform transform)
        {
            _look = look;
            _transform = transform;
        }
        
         void ITickable.Tick() => Rotate();

         private void Rotate()
        {
            if (Mathf.Abs(_lookInputX) < 0.01f)
                return;

            _angle += _lookInputX * RotationSpeed * Time.deltaTime;
            _transform.rotation = Quaternion.Euler(0f, _angle, 0f);
        }
    }
}