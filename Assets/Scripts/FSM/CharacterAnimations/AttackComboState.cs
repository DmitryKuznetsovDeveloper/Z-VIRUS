using Animancer;
using CharacterInput;
using Data;
using UnityEngine;

namespace FSM.CharacterAnimations
{
    public class AttackComboState : IEnterable, IExitable, ITransitionCondition
    {
        private readonly AnimancerComponent _animancer;
        private readonly IAttackInputHandler _input;
        private readonly MeleeComboAnimationSet _comboSet;

        private int _comboIndex;
        private float _lastAttackTime;
        private bool _wasTriggered;

        public AttackComboState(AnimancerComponent animancer, IAttackInputHandler input, MeleeComboAnimationSet comboSet)
        {
            _animancer = animancer;
            _input = input;
            _comboSet = comboSet;
        }

        public void Enter()
        {
            _wasTriggered = false;

            if (Time.time - _lastAttackTime > _comboSet.ComboResetTime)
                _comboIndex = 0;

            _lastAttackTime = Time.time;

            var transition = _comboSet.Attacks[_comboIndex];
            _animancer.Play(transition);

            _comboIndex = (_comboIndex + 1) % _comboSet.Attacks.Length;
        }

        public void Exit()
        {
            _wasTriggered = false;
        }

        public bool CanEnter()
        {
            if (_input.IsLightAttacking && !_wasTriggered)
            {
                _wasTriggered = true;
                return true;
            }

            return false;
        }
    }
}