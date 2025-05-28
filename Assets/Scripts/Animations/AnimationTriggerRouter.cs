using System;
using System.Collections.Generic;
using UnityEngine;

namespace Animations
{
    public class AnimationTriggerRouter : MonoBehaviour
    {
        private readonly Dictionary<string, Action> _triggerHandlers = new();
        
        public void OnAnimationTrigger(string trigger)
        {
            if (_triggerHandlers.TryGetValue(trigger, out var action))
                action.Invoke();
            else
                Debug.LogWarning($"[AnimationTriggerRouter] Нет обработчика для триггера: {trigger}", this);
        }

        public void RegisterTrigger(string trigger, Action callback)
        {
            if (!_triggerHandlers.TryAdd(trigger, callback)) 
                _triggerHandlers[trigger] += callback;
        }

        public void UnregisterTrigger(string trigger, Action callback)
        {
            if (_triggerHandlers.TryGetValue(trigger, out var existing))
            {
                existing -= callback;

                if (existing == null)
                    _triggerHandlers.Remove(trigger);
                else
                    _triggerHandlers[trigger] = existing;
            }
        }

        public void ClearAllTriggers() => _triggerHandlers.Clear();
    }
}

