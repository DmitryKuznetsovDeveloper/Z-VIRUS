using Animancer;
using Data;
using UnityEngine;

namespace Animations.Character
{
    public sealed class MoveAnimatorController
    {
        private MoveAnimationConfig _currentConfig;
        private readonly AnimancerComponent _animancer;
        private AnimancerState _currentState;

        public MoveAnimatorController(AnimancerComponent animancer, MoveAnimationConfig defaultConfig)
        {
            _animancer = animancer;
            _currentConfig = defaultConfig;
        }

        public void SetAnimationConfig(MoveAnimationConfig config)
        {
            _currentConfig = config;
        }

        public void OnMoveAnimation(Vector2 input)
        {
            var clip = _currentConfig.GetByDirection(input);

            if (clip.State?.IsPlaying == true)
                return;

            _currentState = _animancer.Play(clip, 0.25f);
        }

        public void SetAnimationSpeed(float speed)
        {
            if (_currentState != null)
                _currentState.Speed = speed;
        }
    }
}