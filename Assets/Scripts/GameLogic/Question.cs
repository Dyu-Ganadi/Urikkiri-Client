using System;
using Managers;
using TMPro;
using UnityEngine;

namespace GameLogic
{
    public class Question : MonoBehaviour
    {
        public static Question Instance { get; private set; }
        public string question;
        public TextMeshProUGUI questionText;
        public TextMeshProUGUI descriptionText;
        private string _word;

        private void Start()
        {
            Instance = this;
            _word = "___";
        }

        private void Update()
        {
            question = GameStatics.Question.content;
            questionText.text = question.Replace("{}", _word);
            descriptionText.text = GameStatics.IsExaminer()
                ? GameStatics.State.Equals(GameFlowState.CARD_SELECTION)
                    ? "당신은 출제자! 플레이어들이 카드를 고르고 있어요"
                    : "문제에 가장 잘 맞는 카드를 고르세요!"
                : GameStatics.State.Equals(GameFlowState.CARD_SELECTION)
                    ? "당신은 플레이어! 카드를 고르세요"
                    : "출제자가 카드를 고르고 있어요";
        }

        public void SetWord(string word)
        {
            _word = word;
        }
    }
}
