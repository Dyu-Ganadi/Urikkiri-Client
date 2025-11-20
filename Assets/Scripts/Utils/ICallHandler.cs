using UnityEngine;

namespace Utils
{
    public interface ICallHandler
    {
        void HandleEvent(string eventName);
    }
}
