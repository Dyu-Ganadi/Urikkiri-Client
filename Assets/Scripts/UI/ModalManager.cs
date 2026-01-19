using GameLogic;
using TMPro;
using UnityEngine;

namespace UI
{
    [ExecuteAlways]
    public class ModalManager : MonoBehaviour
    {
        public static bool Submitted;
        public void SetSubmitted(bool state) => Submitted = state;
        public Card currentCard;
        
        public void UpdateCard(Card card) => currentCard.index = card.index;

        private void OnEnable()
        {
            if (Submitted) gameObject.SetActive(false);
        }

        public void CloseModal()
        {
            gameObject.SetActive(false);
        }
    }
}
