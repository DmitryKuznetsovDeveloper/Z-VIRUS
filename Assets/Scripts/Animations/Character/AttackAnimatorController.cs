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
        private readonly IIdleResettable _idleReset;
        private readonly System.Random _random = new();
        private readonly object _eventKey = new();

        private bool _isAttacking;
        private bool _isCombatIdle;
        private bool _isInCombat;
        
        private int _lastPunchIndex = -1;
        private int _lastHookIndex = -1;

        private AnimancerLayer _attackLayer => _animancer.Layers.Count > 1 ? _animancer.Layers[1] : _animancer.Layers.Add();

        private AnimancerState _currentAttackState;
        private float _targetWeight;
        private const float WeightFadeSpeed = 5f;

        public bool IsAttacking => _isAttacking;

        public AttackAnimatorController(AnimancerComponent animancer,  IIdleResettable idleReset, AttackAnimationConfig config)
        {
            _animancer = animancer;
            _idleReset = idleReset;
            _config = config;
        }

        public void SetCombatLayerActive(bool active)
        {
            _isInCombat = active;
            _targetWeight = active ? 1f : 0f;

            if (active)
            {
                // 🔥 если атака застряла — сбрасываем
                if (_isAttacking && _currentAttackState?.IsPlaying != true)
                {
                    _isAttacking = false;
                    _currentAttackState = null;
                }

                if (!_isCombatIdle || _currentAttackState?.IsPlaying != true)
                {
                    PlayIdleCombat();
                }
            }

            if (!active && !_isAttacking)
            {
                _isCombatIdle = false;
                _currentAttackState = null;
                _targetWeight = 0f;
            }
        }

        public void Tick() => UpdateLayerWeight();

        private void UpdateLayerWeight()
        {
            float current = _attackLayer.Weight;
            float next = Mathf.MoveTowards(current, _targetWeight, WeightFadeSpeed * Time.deltaTime);
            _attackLayer.Weight = next;

            if (!(next <= 0f) || !(current > 0f)) return;
            _attackLayer.Stop();
            _idleReset.ReapplyIdle();
        }

        public void PlayIdleCombat()
        {
            _currentAttackState = _attackLayer.Play(_config.IdleCombat, 0.2f);
            _currentAttackState.Events(_eventKey).Clear();
            _isCombatIdle = true;

            if (!_currentAttackState.IsLooping)
            {
                SetupReturnToIdle(_currentAttackState);
            }
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

            if (pool == null || pool.Length == 0)
                return;

            // 🧠 Рандом без повтора
            int index = type switch
            {
                AttackType.Punch => GetNonRepeatingIndex(pool.Length, ref _lastPunchIndex),
                AttackType.Hook => GetNonRepeatingIndex(pool.Length, ref _lastHookIndex),
                _ => 0
            };

            var clip = pool[index];

            _currentAttackState = _attackLayer.Play(clip, 0.1f);
            _isAttacking = true;
            _isCombatIdle = false;

            SetupReturnToIdle(_currentAttackState);
        }
        
        private int GetNonRepeatingIndex(int length, ref int lastIndex)
        {
            if (length <= 1)
                return 0;

            int index;
            do
            {
                index = _random.Next(length);
            } while (index == lastIndex);

            lastIndex = index;
            return index;
        }

        private void SetupReturnToIdle(AnimancerState state)
        {
            var events = state.Events(_eventKey);
            events.Clear();
            events.NormalizedEndTime = 1f;

            if (state.IsLooping)
                return;

            events.OnEnd = () =>
            {
                _isAttacking = false;
                _currentAttackState = null;

                if (_isInCombat)
                    PlayIdleCombat();
                else
                {
                    _isCombatIdle = false;
                    _targetWeight = 0f;
                }
            };
        }
    }
}
