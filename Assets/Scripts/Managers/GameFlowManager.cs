using System;
using GameLogic;
using Network;
using UnityEngine;
using Utils;

namespace Managers
{
    public class GameFlowManager : MonoBehaviour, ICallHandler
    {
        public GameObject noticeCanvas;
        public GameObject gameCanvas;
        public GameObject resultCanvas;

        private void Start()
        {
            API.GetRooms().OnResponse(response =>
            {
                GameStatics.RoomData = response;
                noticeCanvas.SetActive(true);
            }).Build();
        }

        private void LoadGame()
        {
            noticeCanvas.SetActive(false);
            API.GetRooms().OnResponse(response =>
            {
                GameStatics.RoomData = response;
                gameCanvas.SetActive(true);
            }).Build();
        }

        public void SubmitCard(Card card)
        {
            // API.SubmitCard(card);
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
