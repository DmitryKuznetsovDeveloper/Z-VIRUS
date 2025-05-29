using System.Collections.Generic;
using Service;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Configs/Animations/MoveAnimationLibrary", fileName = "MoveAnimationLibrary")]
    public sealed class MoveAnimationLibrary : ScriptableObject
    {
        [SerializeField] private List<MovementAnimationSet> _animationSets = new();
    
        public MoveAnimationConfig GetByState(MovementState state, WeaponType weapon)
        {
            // 1. Сначала пробуем точное совпадение
            var config = _animationSets.Find(set => set.State == state && set.Weapon == weapon)?.Config;
            if (config != null)
                return config;

            // 2. Если оружие указано, но конфиг не найден — ищем тот же state без оружия
            if (weapon != WeaponType.None)
            {
                config = _animationSets.Find(set => set.State == state && set.Weapon == WeaponType.None)?.Config;
                if (config != null)
                {
                    UnityEngine.Debug.LogWarning($"[MoveAnimationLibrary] No config for {state} + {weapon}, fallback to {state} + None.");
                    return config;
                }
            }

            // 3. Больше ничего не делаем — пусть возвращается null (Animancer сам не проиграет)
            return null;
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