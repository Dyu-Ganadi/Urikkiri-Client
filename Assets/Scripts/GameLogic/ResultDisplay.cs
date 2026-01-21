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
        public TextMeshProUGUI playerScore;
        
        private void Update()
        {
            var info = GameStatics.FinalScore[index];
            playerName.text = info.nickname;
            playerLevel.text = info.level.ToString();
            playerScore.text = $"+{info.banana_score} 바나나";
        }
    }
}
