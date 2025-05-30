using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Configs/Animations/CharacterAnimationConfigProvider", fileName = "CharacterAnimationConfigProvider")]
    public sealed class CharacterAnimationConfigProvider : ScriptableObject
    {
        public AnimationClip BaseIdle;
        public MoveAnimationConfig Walk;
        public MoveAnimationConfig Run;
    }
}