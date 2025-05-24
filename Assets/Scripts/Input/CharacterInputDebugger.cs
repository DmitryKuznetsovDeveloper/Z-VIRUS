using R3;
using UnityEngine;
using Zenject;

namespace Input
{
    public sealed class CharacterInputDebugger : MonoBehaviour
    {
        private ICharacterInputHandler _inputHandler;
        private CompositeDisposable _disposables;

        [Inject]
        public void Construct(ICharacterInputHandler inputHandler)
        {
            _inputHandler = inputHandler;
        }

        private void OnEnable()
        {
            _disposables = new CompositeDisposable();

            _inputHandler.MoveStream
                .Subscribe(value => Debug.Log($"[Input] Move: {value}"))
                .AddTo(_disposables);

            _inputHandler.LookStream
                .Subscribe(value => Debug.Log($"[Input] Look: {value}"))
                .AddTo(_disposables);

            _inputHandler.SprintStream
                .Subscribe(value => Debug.Log($"[Input] Sprint: {value}"))
                .AddTo(_disposables);

            _inputHandler.ShootStream
                .Subscribe(value => Debug.Log($"[Input] Shoot: {value}"))
                .AddTo(_disposables);
        }

        private void OnDisable()
        {
            _disposables?.Dispose();
        }
    }
}