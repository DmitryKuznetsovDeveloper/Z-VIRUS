using System.Collections.Generic;
using UnityEngine;

namespace Utils.Tween
{
    [CreateAssetMenu(fileName = nameof(TweenAnimationCollection), menuName =  "TweenAnimationConfigs/" + "TweenAnimationCollection",
                    order = 0)]
    public class TweenAnimationCollection : ScriptableObject
    {
        public List<TweenAnimation> Animations;
    }
}
