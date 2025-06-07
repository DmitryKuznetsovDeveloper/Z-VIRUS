using System;
using DG.Tweening;

namespace Utils.Tween
{
    [Serializable]
    public sealed class TweenParamsOut
    {
        public float Duration;
        public float Delay;
        public Ease EaseOut;
    }
    
    [Serializable]
    public sealed class TweenParamsIn
    {
        public float Duration;
        public float Delay;
        public Ease EaseIn;
    }
    
    [Serializable]
    public sealed class TweenParamsOutIn
    {
        public float Duration;
        public float Delay;
        public Ease EaseOut;
        public Ease EaseIn;
    }
}