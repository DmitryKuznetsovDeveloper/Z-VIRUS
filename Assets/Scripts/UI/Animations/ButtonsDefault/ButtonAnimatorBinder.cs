using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Animations.ButtonsDefault
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(ButtonDefaultAnimator))]
    public class ButtonAnimatorBinder : MonoBehaviour, IPointerDownHandler
    {
        private Button _button;
        private ButtonDefaultAnimator _buttonAnimator;
        
        private void Awake()
        {
            _button = GetComponent<Button>();
            _buttonAnimator = GetComponent<ButtonDefaultAnimator>();

            _buttonAnimator.Play(
                _button.interactable
                    ? ButtonDefaultAnimator.NormalPropertyId
                    : ButtonDefaultAnimator.DisabledPropertyId);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if ( !_button.interactable)
                return;
            
            _buttonAnimator.Play(ButtonDefaultAnimator.PressedPropertyId, _button.interactable
                ? ButtonDefaultAnimator.NormalPropertyId
                : ButtonDefaultAnimator.DisabledPropertyId
            );
        }
    }
}