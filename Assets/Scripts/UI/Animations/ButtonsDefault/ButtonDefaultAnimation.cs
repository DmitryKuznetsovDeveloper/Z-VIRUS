using DG.Tweening;
using UnityEngine;
using Utils.Tween;

namespace UI.Animations.ButtonsDefault
{
    [CreateAssetMenu(fileName = nameof(ButtonDefaultAnimation),
        menuName = "TweenAnimationConfigs/Buttons/" + nameof(ButtonDefaultAnimation),
        order = 0)]
    public class ButtonDefaultAnimation : TweenAnimation
    {
        public Color BgColor = Color.white;
        public Color FrameColor = Color.white;
        public Color TextColor = Color.white;
        public float ScaleFactor = 1f;
        public TweenParamsOut Params;

        public override Sequence Create(TweenAnimator tweenView)
        {
            var view = tweenView as ButtonDefaultAnimator;
            var sequence = DOTween.Sequence();
            sequence.Append(view.Root.DOScale(ScaleFactor, Params.Duration).From(1).SetLoops(2, LoopType.Yoyo));
            sequence.Join(view.Bg.DOColor(BgColor, Params.Duration).SetEase(Params.EaseOut));
            sequence.Join(view.Frame.DOColor(FrameColor, Params.Duration).SetEase(Params.EaseOut));
            sequence.Join(view.Text.DOColor(TextColor, Params.Duration).SetEase(Params.EaseOut));
            sequence.SetLink(view.Root.parent.gameObject);
            sequence.SetRecyclable(true);
            sequence.SetAutoKill(false);
            sequence.SetUpdate(true);
            sequence.Pause();
            return sequence;
        }
    }
}