using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Managers;
using NativeWebSocket;
using Newtonsoft.Json;
using UnityEngine;

namespace Network
{
    public class WebSocketClient : MonoBehaviour
    {
        private static WebSocket _websocket;
        private const string ServerUrl = "wss://urikkiri-be.thinkinggms.com/ws?clientType=GAME";

        public bool IsConnected { get; private set; }
        public bool IsConnecting { get; private set; }

        public async void ConnectOn()
        {
            try
            {
                if (_websocket == null && Networking.AccessToken != null) await Connect(Networking.AccessToken);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        private async void OnApplicationPause(bool pauseStatus)
        {
            try
            {
                if (pauseStatus) await DisconnectAsync();
                else ConnectOn();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        public async Task Connect(string authToken = "")
        {
            if (IsConnected || IsConnecting) return;

            IsConnecting = true;

            var additional = "";
            if (!authToken.Equals("")) additional = $"&token={authToken}";
            
            _websocket = new WebSocket(ServerUrl + additional);

            _websocket.OnOpen += () =>
            {
                Debug.Log("Connection open");
                IsConnected = true;
                IsConnecting = false;
            };

            _websocket.OnError += e =>
            {
                Debug.Log(_websocket.State);
                Debug.LogError($"Error: {e}");
                IsConnected = false;
                IsConnecting = false;
            };

            _websocket.OnClose += _ =>
            {
                Debug.Log("Connection closed");
                IsConnected = false;
                IsConnecting = false;
            };

            _websocket.OnMessage += bytes =>
            {
                var message = Encoding.UTF8.GetString(bytes);
                HandleMessage(message);
            };

            await _websocket.Connect();
        }

        private async Task DisconnectAsync()
        {
            if (_websocket != null)
            {
                await _websocket.Close();
                _websocket = null;
            }
        }

        private void Update()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            _websocket?.DispatchMessageQueue();
#endif
        }

        private static void HandleMessage(string message)
        {
            Debug.Log($"Received message: {message}");
            var webSocketMessage = JsonConvert.DeserializeObject<WebSocketMessage<Void>>(message);
            Debug.Log(webSocketMessage.message);
            switch (webSocketMessage.type)
            {
                case WebSocketMessageType.CONNECTED:
                    API.ConnectGame(GameStatics.RoomCode);
                    break;
                case WebSocketMessageType.GAME_START:
                    GameStatics.RoomCode = webSocketMessage.roomCode;
                    GameFlowManager.Instance.GameStart(JsonConvert.DeserializeObject<WebSocketMessage<GameStartData>>(message).data);
                    break;
                case WebSocketMessageType.ALL_CARDS_SUBMITTED:
                    GameStatics.State = GameFlowState.EXAMINER_SELECTION;
                    GameStatics.CardList = JsonConvert.DeserializeObject<WebSocketMessage<CardListResponse>>(message).data;
                    GameCanvasManager.Received = true;
                    break;
                case WebSocketMessageType.EXAMINER_SELECTED:
                    var examinerSelectionDto = JsonConvert.DeserializeObject<WebSocketMessage<ExaminerSelectionDto>>(message).data;
                    GameStatics.GetParticipantInfo(examinerSelectionDto.participant_id).banana_score = examinerSelectionDto.new_banana_score;
                    break;
                case WebSocketMessageType.NEXT_ROUND:
                    GameFlowManager.NextRound(JsonConvert.DeserializeObject<WebSocketMessage<NextRoundResponse>>(message).data);
                    break;
                case WebSocketMessageType.ROUND_END:
                    GameStatics.FinalScore = JsonConvert.DeserializeObject<WebSocketMessage<GameResultDto>>(message).data.rankings.ToArray();
                    Array.Sort(GameStatics.FinalScore, (a, b) => b.rank.CompareTo(a.rank));
                    GameFlowManager.RoundEnd();
                    break;
                default:
                    break;
            }
        }

        public static Task Message(object o)
        {
            return Message(JsonConvert.SerializeObject(o));
        }

        private static async Task Message(string message)
        {
            Debug.Log($"Send: {message}");
            if (_websocket.State == WebSocketState.Open)
            {
                await _websocket.SendText(message);
            }
        }
    }
}
