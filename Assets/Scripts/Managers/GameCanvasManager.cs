using System;
using UI;
using UnityEngine;
using Utils;

namespace Managers
{
    public class GameCanvasManager : SingleMono<GameCanvasManager>
    {
        private static readonly int CardReceived = Animator.StringToHash("card_received");
        private static readonly int ExaminerSelect = Animator.StringToHash("examiner_select");
        public static bool Received;
        public static bool Selected;
        public Animator animator;

        private void OnEnable()
        {
            ModalManager.Submitted = false;
        }

        private void Update()
        {
            if (Received && Instance.animator.GetCurrentAnimatorStateInfo(0).IsName("wait_receive")) Instance.animator.SetTrigger(CardReceived);
            if (Selected && Instance.animator.GetCurrentAnimatorStateInfo(0).IsName("submitted")) Instance.animator.SetTrigger(ExaminerSelect);
        }
    }
}
