using System;
using Managers;
using TMPro;
using UnityEngine;

namespace GameLogic
{
    public class Card : MonoBehaviour
    {
        public int index;
        public CardData cardData;
        public TextMeshProUGUI wordText;
        public TextMeshProUGUI meaningText;

        private void Update()
        {
            cardData = GameStatics.CardList.cards[index];
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
        public long cardId;
        public string word;
        public string meaning;
    }
}
