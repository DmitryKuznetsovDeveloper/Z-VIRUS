using System.Collections.Generic;
using Service;
using UnityEngine;
using MovementState = Service.MovementState;

namespace Data
{
    [CreateAssetMenu(menuName = "Configs/Animations/MoveAnimationLibrary", fileName = "MoveAnimationLibrary")]
    public sealed class MoveAnimationLibrary : ScriptableObject
    {
        [SerializeField] private List<MovementAnimationSet> _animationSets = new();

        public MoveAnimationConfig GetByState(MovementState state, WeaponType weapon)
        {
            var config = _animationSets.Find(set => set.State == state && set.Weapon == weapon)?.Config;
            return config;
        }
    }

    [System.Serializable]
    public class MovementAnimationSet
    {
        public MovementState State;
        public WeaponType Weapon;
        public MoveAnimationConfig Config;
    }
}