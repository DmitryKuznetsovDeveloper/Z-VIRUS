using DG.Tweening;
using UnityEngine;

namespace UI.Animations
{
    public class MoveAndFadeAnimatorController : MonoBehaviour
    {
        [SerializeField] private float _atPosition;
        private IAnimatableShow[] _showMoveFadeAnimations;
        private Sequence _sequenceShow;

        private void Awake() => _showMoveFadeAnimations = GetComponentsInChildren<IAnimatableShow>(true);

        private void OnEnable()
        {
            _sequenceShow?.Kill();
            _sequenceShow = DOTween.Sequence();
            float atPosition = 0;
            for (int i = 0; i < _showMoveFadeAnimations.Length; i++)
            {
                var moveFadeAnimation = _showMoveFadeAnimations[i];
                if (moveFadeAnimation.GameObject.activeInHierarchy)
                {
                    _sequenceShow.InsertCallback(atPosition, () => moveFadeAnimation.Show());
                    atPosition += _atPosition;
                }
            }

            _sequenceShow.SetUpdate(true);
            _sequenceShow.SetLink(gameObject);
            _sequenceShow.Play();
        }

        private void OnDisable() => _sequenceShow?.Kill();

        private void OnDestroy() => _sequenceShow?.Kill();
    }
}