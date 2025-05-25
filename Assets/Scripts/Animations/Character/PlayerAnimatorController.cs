using System;
using Animancer;
using CharacterInput;
using R3;
using UnityEngine;
using Zenject;

namespace Animations.Character
{
    [RequireComponent(typeof(AnimancerComponent))]
    public class PlayerAnimatorController : MonoBehaviour
    {
        [SerializeField] private MixerTransition2D _movementMixer;
        [SerializeField] private ClipTransition _shootClip;

        private AnimancerComponent _animancer;
        private MixerState<Vector2> _moveState;
        private AnimancerLayer _shootLayer;
        private Vector2 _smoothedInput;

        private ICharacterInputHandler _input;
        private IDisposable _moveSub;
        private IDisposable _shootSub;

        [Inject]
        private void Construct(ICharacterInputHandler input) => _input = input;

        private void Awake()
        {
            _animancer = GetComponent<AnimancerComponent>();

            // Движение
            _moveState = _movementMixer.CreateState();
            _animancer.Layers[0].Play(_moveState);

            // Слой выстрела
            _shootLayer = _animancer.Layers[1];
            _shootLayer.Weight = 1f;
            _shootLayer.IsAdditive = false; // true если Additive, но обычно false
        }

        private void OnEnable()
        {
            // Подписка на инпут
            _moveSub = _input.MoveStream
                .Subscribe(OnMove);

            _shootSub = _input.ShootStream
                .Where(x => x)
                .Subscribe(_ => PlayShoot());
        }

        private void OnDisable()
        {
            _moveSub?.Dispose();
            _shootSub?.Dispose();
        }

        private void OnMove(Vector2 input)
        {
            if (input.sqrMagnitude < 0.01f)
            {
                _moveState.IsPlaying = false;
                return;
            }

            input.Normalize();
            _smoothedInput = Vector2.Lerp(_smoothedInput, input, Time.deltaTime * 10f);
            _moveState.Parameter = _smoothedInput;
            _moveState.IsPlaying = true;
        }

        private void PlayShoot()
        {
            var state = _shootLayer.Play(_shootClip);

            var events = state.Events(_shootClip); // ← получить EventSequence по ключу

            events.NormalizedEndTime = 1f;
            events.OnEnd = null;
            events.OnEnd += () => _shootLayer.Stop();
        }


    }
}
