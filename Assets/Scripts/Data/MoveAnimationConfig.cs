using Animancer;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Configs/Animations/MoveAnimationConfig", fileName = "MoveAnimationConfig")]
    public class MoveAnimationConfig : ScriptableObject
    {
        public ClipTransition Idle;
        public ClipTransition Up;
        public ClipTransition Right;
        public ClipTransition Down;
        public ClipTransition Left;
        public ClipTransition UpRight;
        public ClipTransition DownRight;
        public ClipTransition DownLeft;
        public ClipTransition UpLeft;
        
        public ClipTransition GetByDirection(Vector2 dir)
        {
            if (dir == Vector2.zero) return Idle;

            float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
            angle = Mathf.Repeat(angle + 360f, 360f);

            if (angle is < 22.5f or >= 337.5f) return Up;
            if (angle < 67.5f) return UpRight;
            if (angle < 112.5f) return Right;
            if (angle < 157.5f) return DownRight;
            if (angle < 202.5f) return Down;
            if (angle < 247.5f) return DownLeft;
            if (angle < 292.5f) return Left;
            if (angle < 337.5f) return UpLeft;

            return Idle;
        }
    }
}