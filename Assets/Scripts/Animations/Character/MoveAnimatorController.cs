using Animancer;
using Data;
using UnityEngine;

namespace Animations.Character
{
    public sealed class MoveAnimatorController
    {
        private readonly MoveAnimationConfig _moveConfig;
        private readonly AnimancerComponent _animancer;
        private AnimancerState _currentState;

        public MoveAnimatorController(AnimancerComponent animancer, MoveAnimationConfig moveConfig)
        {
            _animancer = animancer;
            _moveConfig = moveConfig;
        }

        public void OnMoveAnimation(Vector2 input)
        {
            var clip = _moveConfig.GetByDirection(input);

            if (clip.State?.IsPlaying == true)
                return;

            _currentState = _animancer.Play(clip, 0.2f);
        }

        public void SetAnimationSpeed(float speed)
        {
            if (_currentState != null)
                _currentState.Speed = speed;
        }
    }
}