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
        public TextMeshProUGUI qa;
        public TextMeshProUGUI questionText;
        public TextMeshProUGUI descriptionText;
        private string _word;

        private void Start()
        {
            Instance = this;
        }

        private void OnEnable()
        {
            SetWord("___");
        }

        private void Update()
        {
            question = GameStatics.Question.content;
            if (GameStatics.State.Equals(GameFlowState.EXAMINER_SELECTED))
            {
                qa.text = "선택된 카드";
                questionText.text = question.Replace("{}", $"<color #FF9B00>{_word}</color>");
                descriptionText.text = $"{GameStatics.SelectionInfo.winner_nickname}의 카드가 출제자의 마음을 울렸다!!";
                return;
            }
            qa.text = "문제";
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
