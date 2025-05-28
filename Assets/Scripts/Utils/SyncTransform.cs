using UnityEngine;

namespace Utils
{
    public class SyncTransform : MonoBehaviour
    {
        [Header("Источник — за кем следовать")]
        [SerializeField] private Transform _target;

        [Header("Приёмник — кого двигать")]
        [SerializeField] private Transform _follower;

        private void LateUpdate() => _follower.position = _target.position;
    }
}