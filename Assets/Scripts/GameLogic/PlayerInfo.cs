using Managers;
using TMPro;
using UnityEngine;

namespace GameLogic
{
    public class PlayerInfo : MonoBehaviour
    {
        public int index;
        public TextMeshProUGUI playerName;
        public TextMeshProUGUI playerLevel;
        public GameObject questioner;
        public GameObject me;
        
        private void Update()
        {
            var info = GameStatics.Users[index];
            questioner.SetActive(false);
            me.SetActive(false);
            if (info.isExaminer) questioner.SetActive(true);
            else if (info.userId == GameStatics.MyUserId) me.SetActive(true);
            playerName.text = info.nickname;
            playerLevel.text = info.level.ToString();
        }
    }
}
