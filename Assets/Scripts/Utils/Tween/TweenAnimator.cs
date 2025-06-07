using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Utils.Tween
{
    public abstract class TweenAnimator : MonoBehaviour
    {
        public AnimatorContainer Container { get; private set; }
        public static readonly int _emptyState = Animator.StringToHash("Empty");
        [SerializeField] private TweenAnimationCollection _animationCollection;
        private Dictionary<int, Sequence> _animations;
        private Dictionary<int, Sequence> Animations
        {
            get
            {
                if (_animations == null)
                    CreateAnimations();
                return _animations;
            }
        }

        protected abstract int InitialAnimationHash { get; }

        public void SetCollection(TweenAnimationCollection collection, bool playFirst = true)
        {
            _animationCollection = collection;
            BuildAnimationDictionary();

            if (playFirst && _animations.TryGetValue(InitialAnimationHash, out var sequence))
                sequence.Restart();
        }

        private void CreateAnimations()
        {
            BuildAnimationDictionary();
            if (_animations.TryGetValue(InitialAnimationHash, out var sequence))
                sequence.Restart();
        }

        private void BuildAnimationDictionary()
        {
            if (_animations == null)
                _animations = new();
            else
            {
                foreach (var seq in _animations.Values)
                    seq.Kill();
                _animations.Clear();
            }

            foreach (var tweenAnimation in _animationCollection.Animations)
            {
                var sequence = tweenAnimation.Create(this);
                _animations.Add(tweenAnimation.Hash, sequence);
            }
        }

        private void OnDisable()
        {
            foreach (var sequence in Animations.Values) 
                sequence.Rewind();
        }

        private void OnDestroy()
        {
            foreach (var sequence in Animations.Values)
                sequence.Kill();
        }
        
        public void Play(string animationName) => PlayInternal(new AnimatorContainer(Animator.StringToHash(animationName)));

        public void Play(string animationName, string onCompleteName) =>
            PlayInternal(new AnimatorContainer(Animator.StringToHash(animationName), Animator.StringToHash(onCompleteName)));

        public void Play(string animationName, Action onComplete) =>
            PlayInternal(new AnimatorContainer(Animator.StringToHash(animationName), onComplete));

        public void Play(int hash) => PlayInternal(new AnimatorContainer(hash));
        
        public void Play(int hash, int onComplete) => PlayInternal(new AnimatorContainer(hash, onComplete));
        
        public void Play(int hash, Action onComplete) => PlayInternal(new AnimatorContainer(hash, onComplete));
        
        public async UniTask PlayAsync(string animationNname, CancellationToken cancellationToken)
        {
            var hash = Animator.StringToHash(animationNname);

            if (!Animations.TryGetValue(hash, out var sequence))
            {
                DebugUtil.LogWarning($"TweenAnimator: missing animation hash {hash}");
                return;
            }

            PauseAnimations();

            var tcs = AutoResetUniTaskCompletionSource.Create();
            
            await using (cancellationToken.Register(() => tcs.TrySetCanceled()))
            {
                sequence.OnComplete(() => tcs.TrySetResult()).Restart();
                await tcs.Task;
            }
        }

        public async UniTask PlayAsync(int hash, CancellationToken cancellationToken)
        {
            if (!Animations.TryGetValue(hash, out var sequence))
            {
                DebugUtil.LogWarning($"TweenAnimator: missing animation hash {hash}");
                return;
            }

            PauseAnimations();

            var tcs = AutoResetUniTaskCompletionSource.Create();
            
            await using (cancellationToken.Register(() => tcs.TrySetCanceled()))
            {
                sequence.OnComplete(() => tcs.TrySetResult()).Restart();
                await tcs.Task;
            }
        }

        private void PlayInternal(AnimatorContainer container)
        {
            Container = container;
            Play(container);
        }

        private void Play(AnimatorContainer animatorContainer)
        {
            if (!Animations.TryGetValue(animatorContainer.MainAnimation, out var sequence))
            {
                DebugUtil.LogWarning("TweenAnimator: missing animation hash " + animatorContainer.MainAnimation);
                return;
            }

            PauseAnimations();

            sequence.OnComplete(() =>
            {
                animatorContainer.OnCompleteAction?.Invoke();
                if (animatorContainer.OnCompleteAnimation != _emptyState)
                    Play(animatorContainer.OnCompleteAnimation);
            }).Restart();
        }

        private void PauseAnimations()
        {
            foreach (var sequence in Animations.Values)
                sequence.Pause();
        }
    }
}