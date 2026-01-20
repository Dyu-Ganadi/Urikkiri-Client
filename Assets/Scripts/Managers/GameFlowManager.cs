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
            GameStatics.GetParticipantInfo(data.new_examiner_id).is_examiner = true;
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

        // ReSharper disable once UnusedMember.Global
        public void SetRoomCode(string code)
        {
            Debug.Log($"룸코드 정상 수신함: {code}");
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
