using System;
using UnityEngine;
using Utils;

namespace Managers
{
    public class GameCanvasManager : SingleMono<GameCanvasManager>
    {
        private static readonly int CardReceived = Animator.StringToHash("card_received");
        private bool _received;
        public Animator animator;
        
        public static void CardReceivedAnimation() => Instance._received = true;

        private void Update()
        {
            if (_received && Instance.animator.GetCurrentAnimatorStateInfo(0).IsName("wait_receive")) Instance.animator.SetTrigger(CardReceived);
        }
    }
}
