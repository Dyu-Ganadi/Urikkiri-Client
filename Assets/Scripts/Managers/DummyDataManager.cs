using System;
using System.Collections;
using System.Collections.Generic;
using GameLogic;
using Network;
using Newtonsoft.Json;
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
                    level = 0,
                    nickname = "유저" + i
                };
                if (i == 0) user.is_examiner = true;
                gs.participants.Add(user);
            }
            StartCoroutine(Delay(1f, () => GameFlowManager.Instance.GameStart(gs)));
            StartCoroutine(Delay(4f, () =>
            {
                var message = "{\"type\":\"ALL_CARDS_SUBMITTED\",\"room_code\":\"333977\",\"data\":[{\"participant_id\":336,\"nickname\":\"수아\",\"card_id\":100,\"word\":\"성금\",\"meaning\":\"말이나 일의 보람이나 효력. 꼭 지켜야 할 명령.\"},{\"participant_id\":335,\"nickname\":\"아임소희\",\"card_id\":88,\"word\":\"볼모\",\"meaning\":\"약속 이행의 담보로 상대편에 잡혀 두는 사람이나 물건. 예전에, 나라 사이에 조약 이행을 담보로 상대국에 억류하여 두던 왕자나 그 밖의 유력한 사람.\"},{\"participant_id\":334,\"nickname\":\"나는이해\",\"card_id\":62,\"word\":\"동부레기\",\"meaning\":\"뿔이 날 만한 나이의 송아지.\"}],\"message\":\"All cards have been submitted\"}";
                JsonConvert.DeserializeObject<WebSocketMessage<object>>(message);
                GameStatics.State = GameFlowState.EXAMINER_SELECTION;
                GameStatics.CardList = new CardListResponse
                {
                    cards = JsonConvert.DeserializeObject<WebSocketMessage<List<CardData>>>(message).data
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
