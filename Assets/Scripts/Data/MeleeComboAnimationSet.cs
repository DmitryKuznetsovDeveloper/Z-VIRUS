using Animancer;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Configs/Animations/MeleeComboSet")]
    public sealed class MeleeComboAnimationSet : ScriptableObject
    {
        [Tooltip("Idle в боевой стойке (рукопашка).")]
        public ClipTransition Idle;
        
        [Tooltip("Список атак в комбо. Они будут проигрываться по очереди.")]
        public ClipTransition[] Attacks;

        [Tooltip("Если следующий удар не происходит в течение этого времени — комбо сбрасывается.")]
        public float ComboResetTime = 1.5f;
    }
}