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
            questioner.SetActive(info.is_examiner);
            me.SetActive(info.IsSelfUser());
            playerName.text = info.nickname;
            playerLevel.text = info.level.ToString();
        }
    }
}
