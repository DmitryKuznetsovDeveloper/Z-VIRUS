using Animancer;
using CharacterInput;
using Data;

namespace FSM.CharacterAnimations
{
    public class RunState : IEnterable, ITickableState, ITransitionCondition
    {
        private readonly AnimancerComponent _animancer;
        private readonly MoveAnimationConfig _runConfig;
        private readonly IMoveInputHandler _input;

        private ClipTransition _currentClip;

        public RunState(AnimancerComponent animancer, MoveAnimationConfig config, IMoveInputHandler input)
        {
            _animancer = animancer;
            _runConfig = config;
            _input = input;
        }

        public void Enter()
        {
            _currentClip = _runConfig.GetByDirection(_input.MoveInput);
            _animancer.Play(_currentClip);
        }

        public void Tick()
        {
            var nextClip = _runConfig.GetByDirection(_input.MoveInput);
            if (_currentClip != nextClip)
            {
                _currentClip = nextClip;
                _animancer.Play(_currentClip,0.2f);
            }

            if (_animancer.States.TryGet(_currentClip, out var state))
                state.Speed = _input.MoveInput.magnitude;
        }


        public bool CanEnter()
        {
            return _input.MoveInput.magnitude > 0.01f && _input.IsSprinting;
        }
    }
}