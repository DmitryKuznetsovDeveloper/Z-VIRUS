using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.EventSystems;

namespace UI
{
    public sealed class ButtonDefaultAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
    {
        [SerializeField] private Image _bg;
        [SerializeField] private Image _frame;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private ButtonStyleConfig _style;
        
        private Vector3 _originalScale;
        private enum ButtonVisualState { Normal, Pressed }
        private void Awake()
        {
            _originalScale = transform.localScale;
            SetVisualState(ButtonVisualState.Normal);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            transform.DOScale(_style.PressedScale, _style.AnimationDuration).SetEase(Ease.OutQuad);
            SetVisualState(ButtonVisualState.Pressed);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            transform.DOScale(_originalScale, _style.AnimationDuration).SetEase(Ease.OutBack);
            SetVisualState(ButtonVisualState.Normal);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.DOScale(_originalScale, _style.AnimationDuration).SetEase(Ease.OutBack);
            SetVisualState(ButtonVisualState.Normal);
        }
        
        private void SetVisualState(ButtonVisualState state)
        {
            switch (state)
            {
                case ButtonVisualState.Normal:
                    _bg.color = _style.BgNormal;
                    _frame.color = _style.FrameNormal;
                    _text.color = _style.TextNormal;
                    break;

                case ButtonVisualState.Pressed:
                    _bg.color = _style.BgPressed;
                    _frame.color = _style.FramePressed;
                    _text.color = _style.TextPressed;
                    break;
            }
        }
    }
    
    [Serializable]
    public sealed class ButtonStyleConfig 
    {
        [Header("Анимация")]
        public float PressedScale = 0.9f;
        public float AnimationDuration = 0.1f;

        [Header("Цвета BG")]
        public Color BgNormal = Color.white;
        public Color BgPressed = new Color(0.85f, 0.85f, 0.85f);

        [Header("Цвета Frame")]
        public Color FrameNormal = Color.white;
        public Color FramePressed = new Color(0.75f, 0.75f, 0.75f);

        [Header("Цвета Текста")]
        public Color TextNormal = Color.white;
        public Color TextPressed = new Color(0.9f, 0.9f, 0.9f);
    }
}
