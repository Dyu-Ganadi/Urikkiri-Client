using Managers;
using TMPro;
using UnityEngine;

namespace GameLogic
{
    public class ResultDisplay : MonoBehaviour
    {
        public int index;
        public TextMeshProUGUI playerName;
        public TextMeshProUGUI playerLevel;
        
        private void Update()
        {
            var info = GameStatics.FinalScore[index];
            playerName.text = info.nickname;
            playerLevel.text = info.level.ToString();
        }
    }
}
