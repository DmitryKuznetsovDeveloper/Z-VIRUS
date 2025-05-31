using Animancer;
using CharacterInput;
using Data;

namespace FSM.CharacterAnimations
{
    public class WalkState : IEnterable, ITickableState, ITransitionCondition
    {
        private readonly AnimancerComponent _animancer;
        private readonly MoveAnimationConfig _walkConfig;
        private readonly IMoveInputHandler _input;

        private ClipTransition _currentClip;

        public WalkState(AnimancerComponent animancer, MoveAnimationConfig config, IMoveInputHandler input)
        {
            _animancer = animancer;
            _walkConfig = config;
            _input = input;
        }

        public void Enter()
        {
            _currentClip = _walkConfig.GetByDirection(_input.MoveInput);
            _animancer.Play(_currentClip,0.2f);
        }

        public void Tick()
        {
            var nextClip = _walkConfig.GetByDirection(_input.MoveInput);
            if (_currentClip != nextClip)
            {
                _currentClip = nextClip;
                _animancer.Play(_currentClip);
            }

            if (_animancer.States.TryGet(_currentClip, out var state))
                state.Speed = _input.MoveInput.magnitude;
        }


        public bool CanEnter()
        {
            return _input.MoveInput.magnitude > 0.01f && !_input.IsSprinting;
        }
    }
}