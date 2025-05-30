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

            var angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
            angle = Mathf.Repeat(angle + 360f, 360f);

            return angle switch
            {
                < 22.5f or >= 337.5f => Up,
                < 67.5f => UpRight,
                < 112.5f => Right,
                < 157.5f => DownRight,
                < 202.5f => Down,
                < 247.5f => DownLeft,
                < 292.5f => Left,
                < 337.5f => UpLeft,
                _ => Idle
            };
        }
    }
}