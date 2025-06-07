using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils.Tween;

namespace UI.Animations.ButtonsDefault
{
    public class ButtonDefaultAnimator :  TweenAnimator
    {
        public static readonly int NormalPropertyId = Animator.StringToHash("Normal");
        public static readonly int PressedPropertyId = Animator.StringToHash("Pressed");
        public static readonly int DisabledPropertyId = Animator.StringToHash("Disabled");

        public Image Bg;
        public Image Frame;
        public TMP_Text Text;
        public RectTransform Root;
        
        protected override int InitialAnimationHash => NormalPropertyId;
    }
}