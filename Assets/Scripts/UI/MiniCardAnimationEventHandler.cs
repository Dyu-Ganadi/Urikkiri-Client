using UnityEngine;

namespace UI
{
    public class MiniCardAnimationEventHandler : MonoBehaviour
    {
        public void HandleEvent(string eventData)
        {
            gameObject.SetActive(false);
        }
    }
}
