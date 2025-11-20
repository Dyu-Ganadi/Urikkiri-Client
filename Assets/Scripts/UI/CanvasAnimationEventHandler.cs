using System;
using UnityEngine;
using Utils;

namespace UI
{
    public class CanvasAnimationEventHandler : MonoBehaviour
    {
        public MonoBehaviour[] callHandlers;
        
        public void HandleEvent(string eventData)
        {
            var data = eventData.Split(':');
            if (int.TryParse(data[0], out var index) && callHandlers[index] is ICallHandler handler)
            {
                handler.HandleEvent(data[1]);
            }
        }
    }
}
