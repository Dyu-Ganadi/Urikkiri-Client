using System.Collections;
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
            Question.Instance.SetWord("___");
            GameStatics.Users = gameStartData.participants.ToArray();
            noticeCanvas.SetActive(true);
        }

        private void LoadGame()
        {
            noticeCanvas.SetActive(false);
            GameCanvasManager.Received = false;
            GameCanvasManager.Selected = false;
            if (GameStatics.IsExaminer()) gameCanvas.SetActive(true);
            else
            {
                API.GetCards().OnResponse(response =>
                {
                    GameStatics.CardList = response;
                    GameCanvasManager.Received = true;
                    gameCanvas.SetActive(true);
                }).Build();
            }
        }

        public static void NextRound(NextRoundResponse data)
        {
            Instance.StartCoroutine(NextRoundFlow(data));
        }

        private static IEnumerator NextRoundFlow(NextRoundResponse data)
        {
            yield return new WaitForSeconds(4f);
            GameStatics.State = GameFlowState.CARD_SELECTION;
            GameStatics.ResetExaminer();
            GameStatics.GetParticipantInfo(data.new_examiner_id).is_examiner = true;
            GameStatics.Question.content = data.quiz.content;
            GameStatics.Question.quiz_id = data.quiz.quiz_id;
            Instance.gameCanvas.SetActive(false);
            Instance.noticeCanvas.SetActive(true);
        }

        public static void RoundEnd()
        {
            Instance.StartCoroutine(RoundEndFlow());
        }

        private static IEnumerator RoundEndFlow()
        {
            yield return new WaitForSeconds(4f);
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

        public void LeaveRoom()
        {
            API.LeaveRoom(GameStatics.RoomCode);
        }

        public void HandleEvent(string eventName)
        {
            switch (eventName)
            {
                case "game_load": LoadGame();
                    break;
                case "select_examiner":
                    break;
            }
        }
    }
}
