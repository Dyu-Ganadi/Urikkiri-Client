using System;
using Managers;
using UnityEngine;

namespace GameLogic
{
    public class CardList : MonoBehaviour
    {
        public Card[] cards;
        private int _length;

        public void Update()
        {
            if (GameStatics.CardList == null || _length == GameStatics.CardList.cards.Count) return;
            _length = GameStatics.CardList.cards.Count;
            for (var i = 0; i < cards.Length; i++)
            {
                cards[i].gameObject.SetActive(true);
                if (i >= _length) cards[i].gameObject.SetActive(false);
            }
        }
    }
}
