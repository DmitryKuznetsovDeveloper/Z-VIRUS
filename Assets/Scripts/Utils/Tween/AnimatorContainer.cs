using System;

namespace Utils.Tween
{
    public readonly struct AnimatorContainer
    {
        public readonly int MainAnimation;
        public readonly int OnCompleteAnimation;
        public readonly Action OnCompleteAction;

        public AnimatorContainer(int mainAnimation, int onCompleteAnimation)
        {
            MainAnimation = mainAnimation;
            OnCompleteAnimation = onCompleteAnimation;
            OnCompleteAction = null;
        }

        public AnimatorContainer(int mainAnimation)
        {
            MainAnimation = mainAnimation;
            OnCompleteAnimation = TweenAnimator._emptyState;
            OnCompleteAction = null;
        }
        
        public AnimatorContainer(int mainAnimation, Action onComplete)
        {
            MainAnimation = mainAnimation;
            OnCompleteAnimation = TweenAnimator._emptyState;
            OnCompleteAction = onComplete;
        }
    }
}