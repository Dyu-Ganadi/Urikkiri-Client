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
            networking.SetAccessToken("eyJ0eXBlIjoiYWNjZXNzIiwiYWxnIjoiSFM1MTIifQ.eyJzdWIiOiJtaW5kaW5nMjc5NkB0aGlua2luZ2dtcy5jb20iLCJpYXQiOjE3Njg4MjEwMTEsImV4cCI6MTc2OTAwMTAxMX0.wcf1R7UL1nC-AuaIUvipn4W8fajsFcfZWE5Egc5zufuvFS5_WCUzXZkHrrCD33IxNBwZkGCi1YfaO0SGMnifaw");
            var gs = new GameStartData
            {
                participants = new List<ParticipantInfo>(),
                question = new QuizResponse
                {
                    content = "{} 으악 {}",
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
                    level = 0,
                    nickname = "유저" + i
                };
                if (i == 0) user.is_examiner = true;
                gs.participants.Add(user);
            }
            StartCoroutine(Delay(1f, () => GameFlowManager.Instance.GameStart(gs)));
            StartCoroutine(Delay(5f, () =>
            {
                GameStatics.State = GameFlowState.EXAMINER_SELECTION;
                GameStatics.CardList = new CardListResponse
                {
                    cards = new List<CardData>
                    {
                        new()
                        {
                            card_id = 0,
                            word = "뭐시기",
                            meaning = "히히",
                            participant_id = 6
                        },
                        new()
                        {
                            card_id = 1,
                            word = "저시기",
                            meaning = "히히",
                            participant_id = 7
                        },
                        new()
                        {
                            card_id = 2,
                            word = "이런",
                            meaning = "아아",
                            participant_id = 8
                        }
                    }
                };
                GameCanvasManager.Received = true;
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
