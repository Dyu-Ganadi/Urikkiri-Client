using System;
using TMPro;
using UnityEngine;

namespace GameLogic
{
    [ExecuteAlways]
    public class Card : MonoBehaviour
    {
        public CardData cardData;
        public TextMeshProUGUI wordText;
        public TextMeshProUGUI meaningText;

        [ExecuteAlways]
        private void Update()
        {
            wordText.text = cardData.word;
            meaningText.text = cardData.meaning;
        }

        public void Hover()
        {
            Question.Instance.SetWord(cardData.word);
        }
    }

    [Serializable]
    public class CardData
    {
        public string cardId;
        public string word;
        public string meaning;
    }
}
