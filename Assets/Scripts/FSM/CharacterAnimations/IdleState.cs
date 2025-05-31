using Animancer;
using CharacterInput;
using UnityEngine;

namespace FSM.CharacterAnimations
{
    public class IdleState : IEnterable, ITransitionCondition
    {
        private readonly AnimancerComponent _animancer;
        private readonly AnimationClip _clip;
        private readonly IMoveInputHandler _input;

        public IdleState(AnimancerComponent animancer, AnimationClip clip, IMoveInputHandler input)
        {
            _animancer = animancer;
            _clip = clip;
            _input = input;
        }

        public void Enter() => _animancer.Play(_clip,0.2f);

        public bool CanEnter() => _input.MoveInput == Vector2.zero;
    }
}