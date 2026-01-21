using System;
using JetBrains.Annotations;
using Managers;
using TMPro;
using UnityEngine;

namespace GameLogic
{
    public class Card : MonoBehaviour
    {
        private static readonly int DoFlip = Animator.StringToHash("doFlip");
        public Animator animator;
        public int index;
        public bool flipped;
        public CardData cardData;
        public TextMeshProUGUI wordText;
        public TextMeshProUGUI meaningText;

        private void Update()
        {
            if (flipped) animator.SetTrigger(DoFlip);
            if (GameStatics.CardList == null || index >= GameStatics.CardList.cards.Count) return;
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
        // ReSharper disable InconsistentNaming
        public long card_id;
        public string word;
        public string meaning;
        public long participant_id;
        // ReSharper restore InconsistentNaming
    }
}
