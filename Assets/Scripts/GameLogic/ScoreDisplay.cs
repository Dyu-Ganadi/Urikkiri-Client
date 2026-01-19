using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameLogic
{
    public class ScoreDisplay : MonoBehaviour
    {
        public int index;
        public TextMeshProUGUI playerName;
        public TextMeshProUGUI playerScore;
        public Image me;
        public Image bananaImage;
        public Sprite[] bananaSprites;
        
        private void Update()
        {
            var info = GameStatics.Users[index];
            me.enabled = info.userId == GameStatics.MyUserId;
            playerName.text = info.nickname;
            playerScore.text = $"바나나 {info.bananaScore}개";
            bananaImage.sprite = bananaSprites[info.bananaScore];
        }
    }
}
