using Animancer;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Configs/Animations/AttackAnimationConfig", fileName = "AttackAnimationConfig")]
    public class AttackAnimationConfig : ScriptableObject
    {
        public ClipTransition IdleCombat;
        public ClipTransition[] Punches;
        public ClipTransition[] Hooks;
    }
}