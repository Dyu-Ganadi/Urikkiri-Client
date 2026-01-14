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

        private void Start()
        {
            Instance = this;
            questionText.text = question.Replace("{}", "___");
        }

        private void Update()
        {
            question = GameStatics.Question.content;
        }

        public void SetWord(string word)
        {
            questionText.text = question.Replace("{}", word);
        }
    }
}
