using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI
{
    public class SliderAnimation : MonoBehaviour
    {
        [Header("Slider Settings")]
        [SerializeField] private TMP_Text _loadingLabel;
        [SerializeField] private RectTransform _decorMove;
        [SerializeField] private CanvasGroup _gradientCanvasGroup;
        [SerializeField] private RectTransform _gradientRectTransform;
        [SerializeField] private float _delayGradientSlider = 0.3f;
        [SerializeField] private float _durationSlider = 1f;
        [SerializeField] private Ease _easeSlider = Ease.InOutSine;

        private readonly Vector2 _startGradientAnchorsMin = new(0, 0);
        private readonly Vector2 _endGradientAnchorsMin = new(1, 0);
        private readonly Vector2 _startGradientAnchorsMax = new(0, 1);
        private readonly Vector2 _endGradientAnchorsMax = new(1, 1);
        private readonly Vector2 _startDecorMove = new(0f, 0.5f);
        private readonly Vector2 _endDecorMove = new(1f, 0.5f);

        private Sequence _sequence;

        private void Awake()
        {
            _sequence = DOTween.Sequence().SetUpdate(true).SetAutoKill(false).SetEase(_easeSlider).SetLoops(-1, LoopType.Restart);
            _sequence.SetLink(gameObject);

            // Появление
            _sequence.Append(_gradientCanvasGroup.DOFade(1, _durationSlider).From(0f));
            _sequence.Join(_loadingLabel.DOFade(1f, _durationSlider).From(0f));
            _sequence.Join(_gradientRectTransform.DOAnchorMax(_endGradientAnchorsMax, _durationSlider).From(_startGradientAnchorsMax));

            // Смещение градиента и декора
            _sequence.Append(_gradientRectTransform.DOAnchorMin(_endGradientAnchorsMin, _durationSlider)
                .From(_startGradientAnchorsMin)
                .SetDelay(_delayGradientSlider));

            _sequence.Join(_decorMove.DOAnchorMin(_endDecorMove, _durationSlider).From(_startDecorMove));
            _sequence.Join(_decorMove.DOAnchorMax(_endDecorMove, _durationSlider).From(_startDecorMove));
            _sequence.Join(_decorMove.DOPivot(_endDecorMove, _durationSlider).From(_startDecorMove));

            // Исчезновение
            _sequence.Join(_loadingLabel.DOFade(0f, _durationSlider).SetDelay(_delayGradientSlider));
            _sequence.Join(_gradientCanvasGroup.DOFade(0f, _durationSlider));

            _sequence.SetUpdate(true);
            _sequence.SetRecyclable(true);
            _sequence.SetAutoKill(false);
            _sequence.Pause();
        }

        private void OnEnable() => _sequence?.Restart();

        private void OnDisable() => _sequence?.Rewind();

        private void OnDestroy() => _sequence?.Kill();
    }
}
