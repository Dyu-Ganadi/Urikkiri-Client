using System;
using UI;
using UnityEngine;
using Utils;

namespace Managers
{
    public class GameCanvasManager : SingleMono<GameCanvasManager>
    {
        private static readonly int CardReceived = Animator.StringToHash("card_received");
        public static bool Received;
        public Animator animator;

        private void OnEnable()
        {
            ModalManager.Submitted = false;
        }

        private void Update()
        {
            if (Received && Instance.animator.GetCurrentAnimatorStateInfo(0).IsName("wait_receive")) Instance.animator.SetTrigger(CardReceived);
        }
    }
}
