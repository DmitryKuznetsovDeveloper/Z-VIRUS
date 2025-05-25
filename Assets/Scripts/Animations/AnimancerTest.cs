using Animancer;
using UnityEngine;

namespace Animations
{
    public class AnimancerTest : MonoBehaviour
    {
        [SerializeField] private AnimancerComponent _animancer;

        [Header("Анимации")]
        [SerializeField] private ClipTransition _idle;
        [SerializeField] private ClipTransition _walk;

        private void Start()
        {
            PlayIdle();
        }

        private void Update()
        {
            if (UnityEngine.Input.GetKey(KeyCode.W))
                _animancer.Play(_walk);
            else
                _animancer.Play(_idle);
        }

        private void PlayIdle() => _animancer.Play(_idle);
    }
}