using Animancer;
using Data;
using UnityEngine;

namespace Animations.Character
{
    public sealed class MoveAnimatorController : IIdleResettable
    {
        private readonly AnimancerComponent _animancer;
        private MoveAnimationConfig _currentConfig;
        private AnimancerState _currentState;

        public MoveAnimatorController(AnimancerComponent animancer, MoveAnimationConfig config)
        {
            _animancer = animancer;
            _currentConfig = config;
        }

        public void SetAnimationConfig(MoveAnimationConfig config)
        {
            _currentConfig = config;
        }

        public void OnMoveAnimation(Vector2 input)
        {
            var clip = _currentConfig.GetByDirection(input);
            if (_animancer.States.TryGet(clip, out var state) && state.IsPlaying is true and true)
            {
                _currentState = state;
                return;
            }
            _currentState = _animancer.Play(clip, 0.2f);
        }
        
        public void SetAnimationSpeed(float speed)
        {
            if (_currentState != null)
                _currentState.Speed = speed;
        }
        
        public void ReapplyIdle() => OnMoveAnimation(Vector2.zero);
    }
}