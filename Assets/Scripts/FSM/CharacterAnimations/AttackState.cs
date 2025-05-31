using Animancer;
using CharacterInput;
using UnityEngine;

namespace FSM.CharacterAnimations
{
    public sealed class AttackState : IEnterable, IExitable, ITransitionCondition
    {
        private readonly AnimancerComponent _animancer;
        private readonly IAttackInputHandler _input;
        private readonly AnimationClip[] _attacks;

        private int _comboIndex;
        private float _lastAttackTime;
        private ClipState _state;

        private const int Layer = 0;
        private readonly float _comboResetTime;

        public bool IsPlaying { get; private set; }

        public AttackState(AnimancerComponent animancer, IAttackInputHandler input, AnimationClip[] attacks, float comboResetTime)
        {
            _animancer = animancer;
            _input = input;
            _attacks = attacks;
            _comboResetTime = comboResetTime;
        }

        public void Enter()
        {
            IsPlaying = true;

            if (Time.time - _lastAttackTime > _comboResetTime)
                _comboIndex = 0;

            _lastAttackTime = Time.time;

            var clip = _attacks[_comboIndex];
            _state = _animancer.Layers[Layer].GetOrCreateState(clip) as ClipState;
            if (_state == null)
            {
                Debug.LogError("Attack clip is not a ClipState");
                return;
            }

            _animancer.Play(_state, 0.1f);

            _state.Events(_state.Clip).OnEnd = () =>
            {
                _comboIndex = (_comboIndex + 1) % _attacks.Length;
                IsPlaying = false;
            };
        }

        public void Exit()
        {
            _animancer.Layers[Layer].StartFade(0f, 0.2f);
            IsPlaying = false;
        }

        public bool CanEnter() => !IsPlaying && _input.IsLightAttacking;
    }
}