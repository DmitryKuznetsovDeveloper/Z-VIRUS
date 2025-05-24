using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public sealed class LoadingScreenView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _loadingText;
        [SerializeField] private TMP_Text _pressAnyKeyText;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Slider _slider;
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private List<Sprite> _backgrounds;
        [SerializeField] private float _switchInterval = 3f;

        private Tween _progressTween;
        private bool _isVisible;
        private int _lastBackgroundIndex = -1;

        private void Awake() => gameObject.SetActive(false);

        public void SetVisible(bool visible)
        {
            if (visible)
            {
                ResetPressAnyKeyLabel();
                gameObject.SetActive(true);
                _canvasGroup.blocksRaycasts = true;
                _canvasGroup.DOFade(1f, 0.2f);
                _isVisible = true;
                StartBackgroundLoop().Forget();
            }
            else
            {
                _canvasGroup.DOFade(0f, 0.2f).OnComplete(() =>
                {
                    _canvasGroup.blocksRaycasts = false;
                    gameObject.SetActive(false);
                    _isVisible = false;
                });
            }
        }

        public void SetProgress(float value)
        {
            _progressTween?.Kill();
            _slider.value = value;
        }

        public void SetProgressAnimated(float value)
        {
            _progressTween?.Kill();
            _progressTween = _slider
                .DOValue(value, 0.24f)
                .SetEase(Ease.OutQuad)
                .SetUpdate(true);
        }
        
        public void ShowPressAnyKey()
        {
            _loadingText.DOFade(0, 0.2f).SetEase(Ease.Linear).From(1);
            _pressAnyKeyText.DOFade(1f, 0.4f).From(0).SetEase(Ease.Linear);
        }

        private void ResetPressAnyKeyLabel()
        {
            _pressAnyKeyText.DOFade(0f, 0f).From(0);
            _loadingText.DOFade(1f, 0f).From(1);
        }

        private async UniTaskVoid StartBackgroundLoop()
        {
            SetRandomBackground();

            while (_isVisible)
            {
                await UniTask.Delay((int)(_switchInterval * 1000));
                if (!_isVisible) break;
                SetRandomBackground();
            }
        }

        private void SetRandomBackground()
        {
            if (_backgrounds.Count == 0) return;

            int index;
            do
            {
                index = Random.Range(0, _backgrounds.Count);
            } while (_backgrounds.Count > 1 && index == _lastBackgroundIndex);

            _backgroundImage.sprite = _backgrounds[index];
            _lastBackgroundIndex = index;
        }
    }
}
