using System;
using System.Collections;
using System.Collections.Generic;
using GameLogic;
using Network;
using UnityEngine;

namespace Managers
{
    public class DummyDataManager : MonoBehaviour
    {
        public Networking networking;
        private void Start()
        {
#if UNITY_EDITOR
            // 더미 데이터
            GameFlowManager.Instance.SetRoomCode("973495");
            networking.SetAccessToken("eyJ0eXBlIjoiYWNjZXNzIiwiYWxnIjoiSFM1MTIifQ.eyJzdWIiOiJ1cmlra2lyaUB0aGlua2luZ2dtcy5jb20iLCJpYXQiOjE3NjkwNDg2MTEsImV4cCI6MTc2OTIyODYxMX0.kfy_MalLortR7PK3ou-PhzzagtVm-oj6Coxnjs4sO0uxyFuh2ubVmL6DvsUdqdsa39zTFT4VwNAmvxIe1BRpQg");
            var gs = new GameStartData
            {
                participants = new List<ParticipantInfo>(),
                question = new QuizResponse
                {
                    content = "으악! {}(이)다!",
                    quiz_id = 1
                }
            };
            for (var i = 0; i < 4; i++)
            {
                var user = new ParticipantInfo
                {
                    user_id = i + 5,
                    banana_score = 0,
                    is_examiner = false,
                    card_submitted = true,
                    level = 0,
                    nickname = "유저" + i
                };
                if (i == 0) user.is_examiner = true;
                gs.participants.Add(user);
            }
            StartCoroutine(Delay(1f, () => GameFlowManager.Instance.GameStart(gs)));
            StartCoroutine(Delay(10f, () =>
            {
                Debug.Log("examiner selected");
                var examinerSelectionDto = new ExaminerSelectionDto
                {
                    user_id = 5,
                    selected_card = new CardData
                    {
                        card_id = 1,
                        meaning = "흐헤헤",
                        word = "즐겁다는 뜻이다"
                    },
                    winner_nickname = "우?끾끼",
                    new_banana_score = 2
                };
                GameStatics.SelectionInfo = examinerSelectionDto;
                GameStatics.ResetSubmitted();
                GameStatics.State = GameFlowState.EXAMINER_SELECTED;
                GameStatics.CardList.cards[0] = examinerSelectionDto.selected_card;
                GameStatics.GetParticipantInfo(examinerSelectionDto.user_id).banana_score = examinerSelectionDto.new_banana_score;
                Question.Instance.SetWord(examinerSelectionDto.selected_card.word);
                GameCanvasManager.Selected = true;
            }));
#endif
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private static IEnumerator Delay(float delay, Action action = null) {
            yield return new WaitForSeconds(delay);
            action?.Invoke();
        }
    }
}
