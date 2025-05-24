using DG.Tweening;
using UnityEngine;

namespace UI.Animations
{
    public class MoveAndFadeAnimator : MonoBehaviour, IAnimatableShow
    {
        public GameObject GameObject => gameObject;
        
        [SerializeField] private RectTransform _root;
        [SerializeField] private CanvasGroup _rootCanvasGroup;
        [Space(10)] [SerializeField] private bool _isShowOnEnable;
        [SerializeField] private Vector2 _fromPosition;
        [SerializeField] private Vector2 _endPosition;
        [SerializeField] private float _duration;
        [SerializeField] private Ease _ease;

        private Sequence _sequenceShow;

        private void Awake() => InitSequence();

        private void OnEnable()
        {
            if (_isShowOnEnable)
                Show();
        }

        private void OnDisable() => _sequenceShow?.Rewind();

        private void OnDestroy() => _sequenceShow?.Kill();

        public void Show() => _sequenceShow?.Restart();

        private void InitSequence()
        {
            _sequenceShow = DOTween.Sequence();
            _sequenceShow.Append(_root.DOAnchorPos(_endPosition, _duration).From(_fromPosition));
            _sequenceShow.Insert(0.2f, _rootCanvasGroup.DOFade(1, _duration).From(0));
            _sequenceShow.SetRecyclable(true);
            _sequenceShow.SetEase(_ease);
            _sequenceShow.SetUpdate(true);
            _sequenceShow.SetAutoKill(false);
            _sequenceShow.SetLink(gameObject);
            _sequenceShow.Pause();
        }
    }
}