using Managers;
using TMPro;
using UI;
using UnityEngine;

namespace GameLogic
{
    public class PlayerInfo : MonoBehaviour
    {
        private static readonly int Reduce = Animator.StringToHash("reduce");
        public int index;
        public TextMeshProUGUI playerName;
        public TextMeshProUGUI playerLevel;
        public GameObject questioner;
        public GameObject me;
        public Animator cardAnimator;
        
        private void Update()
        {
            var info = GameStatics.Users[index];
            questioner.SetActive(info.is_examiner);
            me.SetActive(info.IsSelfUser());
            if (info.card_submitted) cardAnimator.gameObject.SetActive(true);
            else cardAnimator.SetTrigger(Reduce);
            playerName.text = info.nickname;
            playerLevel.text = info.level.ToString();
        }
    }
}
