using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace Utils.Tween
{
    public class TweenAnimation : ScriptableObject
    {
        public string Name;
        [ReadOnly] public int Hash;
        public virtual Sequence Create(TweenAnimator view) => default;
        public void OnValidate()
        {
            Hash = Animator.StringToHash(Name);
        }
    }
}