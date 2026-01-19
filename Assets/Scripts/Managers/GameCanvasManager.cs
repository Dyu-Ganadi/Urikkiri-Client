using UnityEngine;
using Utils;

namespace Managers
{
    public class GameCanvasManager : SingleMono<GameCanvasManager>
    {
        private static readonly int CardReceived = Animator.StringToHash("card_received");
        public Animator animator;
        
        public static void CardReceivedAnimation() => Instance.animator.SetTrigger(CardReceived);
    }
}
