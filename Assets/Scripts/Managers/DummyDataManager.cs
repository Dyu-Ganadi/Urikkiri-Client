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
                    level = 0,
                    nickname = "유저" + i
                };
                if (i == 0) user.is_examiner = true;
                gs.participants.Add(user);
            }
            StartCoroutine(Delay(1f, () => GameFlowManager.Instance.GameStart(gs)));
#endif
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private static IEnumerator Delay(float delay, Action action = null) {
            yield return new WaitForSeconds(delay);
            action?.Invoke();
        }
    }
}
