using UnityEngine;

namespace Animations
{
    public class DebugTrigger : MonoBehaviour
    {
        public void OnAnimationTrigger(string msg)
        {
            Debug.Log($"🔔 Сработал триггер: {msg}");
        }
    }
}