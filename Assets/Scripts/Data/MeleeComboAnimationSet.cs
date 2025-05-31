using Animancer;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Configs/Animations/MeleeComboSet")]
    public sealed class MeleeComboAnimationSet : ScriptableObject
    {
        public ClipTransition Idle;
        public ClipTransition[] Attacks;
        public float ComboResetTime = 1.5f;
    }
}