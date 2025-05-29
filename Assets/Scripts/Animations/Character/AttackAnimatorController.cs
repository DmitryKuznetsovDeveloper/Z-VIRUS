using Animancer;
using CharacterInput;
using Data;
using UnityEngine;
using Zenject;

namespace Animations.Character
{
    public sealed class AttackAnimatorController : ITickable
    {
        private readonly AnimancerComponent _animancer;
        private readonly AttackAnimationConfig _config;
        private readonly System.Random _random = new();
        private readonly object _eventKey = new();
        private bool _isAttacking;

        private AnimancerLayer _attackLayer => _animancer.Layers.Count > 1 ? _animancer.Layers[1] : _animancer.Layers.Add();

        private AnimancerState _currentAttackState;
        private float _targetWeight;
        private const float WeightFadeSpeed = 5f;

        public AttackAnimatorController(AnimancerComponent animancer, AttackAnimationConfig config)
        {
            _animancer = animancer;
            _config = config;
        }

        public void SetCombatLayerActive(bool active)
        {
            _targetWeight = active ? 1f : 0f;
            if (active && !_isAttacking)
                PlayIdleCombat();

            if (!active)
            {
                _attackLayer.Stop();
                _isAttacking = false;
            }
        }
        
        public void PlayIdleCombat()
        {
            _currentAttackState = _attackLayer.Play(_config.IdleCombat, 0.2f);
            _currentAttackState.Events(_eventKey).Clear();
        }

        public void PlayAttack(AttackType type)
        {
            if (_isAttacking)
                return;

            var pool = type switch
            {
                AttackType.Punch => _config.Punches,
                AttackType.Hook => _config.Hooks,
                _ => null
            };

            if (pool == null || pool.Length == 0) return;

            int index = _random.Next(0, pool.Length);
            _currentAttackState = _attackLayer.Play(pool[index], 0.1f);
            _isAttacking = true;

            SetupReturnToIdle(_currentAttackState);
        }
        
        private void SetupReturnToIdle(AnimancerState state)
        {
            var events = state.Events(_eventKey);
            events.Clear();
            events.NormalizedEndTime = 1f;
            events.OnEnd = () =>
            {
                _isAttacking = false;
                PlayIdleCombat();
            };
        }
        
        private void UpdateLayerWeight()
        {
            var current = _attackLayer.Weight;
            _attackLayer.Weight = Mathf.MoveTowards(current, _targetWeight, WeightFadeSpeed * Time.deltaTime);
        }
        void ITickable.Tick() => UpdateLayerWeight();
    }
}
