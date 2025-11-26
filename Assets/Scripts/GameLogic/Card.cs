using System;
using TMPro;
using UnityEngine;

namespace GameLogic
{
    [ExecuteAlways]
    public class Card : MonoBehaviour
    {
        public CardData cardData;
        public TextMeshProUGUI cardNameText;
        public TextMeshProUGUI descriptionText;

        [ExecuteAlways]
        private void Update()
        {
            cardNameText.text = cardData.cardName;
            descriptionText.text = cardData.description;
        }

        public void Hover()
        {
            Question.Instance.SetWord(cardData.cardName);
        }
    }

    [Serializable]
    public class CardData
    {
        public string cardId;
        public string cardName;
        public string description;
    }
}
