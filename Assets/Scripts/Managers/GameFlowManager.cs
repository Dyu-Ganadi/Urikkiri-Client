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
            if (GameStatics.IsExaminer())
            {
                gameCanvas.SetActive(true);
            }
            else
            {
                API.GetCards().OnResponse(response =>
                {
                    GameStatics.CardList = response;
                    GameCanvasManager.CardReceivedAnimation();
                    gameCanvas.SetActive(true);
                }).Build();
            }
        }

        public static void NextRound(NextRoundResponse data)
        {
            GameStatics.State = GameFlowState.CARD_SELECTION;
            GameStatics.ResetExaminer();
            GameStatics.GetParticipantInfo(data.newExaminerId).isExaminer = true;
            GameStatics.Question.content = data.quiz.content;
            GameStatics.Question.quizId = data.quiz.quizId;
            Instance.gameCanvas.SetActive(false);
            Instance.noticeCanvas.SetActive(true);
        }

        public static void RoundEnd()
        {
            Instance.gameCanvas.SetActive(false);
            Instance.resultCanvas.SetActive(true);
        }

        public static void SetRoomCode(string code)
        {
            GameStatics.RoomCode = code;
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
