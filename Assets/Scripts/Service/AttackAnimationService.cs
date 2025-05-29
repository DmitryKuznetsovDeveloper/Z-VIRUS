using System;
using Animations.Character;
using CharacterInput;
using R3;
using Zenject;

namespace Service
{
    public sealed class AttackAnimationService : IInitializable, IDisposable
    {
        private readonly IAttackInputHandler _input;
        private readonly AttackAnimatorController _animator;
        private IDisposable _attackSub;
        private IDisposable _meleeModeSub;

        public AttackAnimationService(IAttackInputHandler input, AttackAnimatorController animator)
        {
            _input = input;
            _animator = animator;
        }

        public void Initialize()
        {
            _meleeModeSub = _input.MeleeModeStream.Subscribe(_animator.SetCombatLayerActive);
            _attackSub = _input.AttackStream.Subscribe(_animator.PlayAttack);
        }

        public void Dispose()
        {
            _attackSub?.Dispose();
            _meleeModeSub?.Dispose();
        }
    }
}