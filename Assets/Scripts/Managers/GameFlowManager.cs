using System;
using GameLogic;
using Network;
using UnityEngine;
using Utils;

namespace Managers
{
    public class GameFlowManager : SingleMono<GameFlowManager>, ICallHandler
    {
        public GameObject noticeCanvas;
        public GameObject gameCanvas;
        public GameObject resultCanvas;

        public void GameStart(GameStartData gameStartData)
        {
            GameStatics.Question = gameStartData.question;
            GameStatics.Users = gameStartData.participants.ToArray();
            API.GetMyData().OnResponse(res => GameStatics.MyUserId = res.id).Build();
            noticeCanvas.SetActive(true);
        }

        private void LoadGame()
        {
            noticeCanvas.SetActive(false);
            API.GetCards().OnResponse(response =>
            {
                GameStatics.CardList = response;
                gameCanvas.SetActive(true);
            }).Build();
        }

        public void SubmitCard(Card card)
        {
            API.SubmitCard(card);
        }

        public void HandleEvent(string eventName)
        {
            switch (eventName)
            {
                case "game_load": LoadGame();
                    break;
            }
        }
    }
}
