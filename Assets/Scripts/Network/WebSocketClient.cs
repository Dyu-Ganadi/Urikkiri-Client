using System;
using System.Collections.Generic;
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
        private const string ServerUrl = "wss://urikkiri-be.thinkinggms.com/ws";

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

            var dictionary = new Dictionary<string, string>();
            if (!authToken.Equals("")) dictionary.Add("Authorization", $"Bearer {authToken}");

            _websocket = new WebSocket(ServerUrl, dictionary);

            _websocket.OnOpen += () =>
            {
                Debug.Log("Connection open");
                IsConnected = true;
                IsConnecting = false;
            };

            _websocket.OnError += e =>
            {
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
            var webSocketMessage = JsonConvert.DeserializeObject<WebSocketMessage>(message);
            Debug.Log(webSocketMessage.message);
            switch (webSocketMessage.type)
            {
                case WebSocketMessageType.GAME_START:
                    GameStatics.RoomCode = webSocketMessage.roomCode;
                    GameFlowManager.Instance.GameStart(JsonConvert.DeserializeObject<GameStartData>(webSocketMessage.data));
                    break;
                case WebSocketMessageType.ALL_CARDS_SUBMITTED:
                    Debug.Log("All Cards Submitted");
                    break;
                case WebSocketMessageType.SUBMIT_CARD:
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
