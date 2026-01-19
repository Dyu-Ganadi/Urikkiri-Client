using System;
using System.Threading.Tasks;
using GameLogic;
using JetBrains.Annotations;
using Managers;
using Newtonsoft.Json;
using UnityEngine;

namespace Network
{
    public static class API
    {
        public static Networking.Get<CardListResponse> GetCards()
        {
            return new Networking.Get<CardListResponse>("/play-together/cards");
        }
        
        public static Networking.Get<MyPageResponse> GetMyData()
        {
            return new Networking.Get<MyPageResponse>("/users/my");
        }

        public static Task SubmitCard(Card card)
        {
            return WebSocketClient.Message(GameStatics.IsExaminer()
                ? new WebSocketRequestMessage(WebSocketMessageType.EXAMINER_SELECT, GameStatics.RoomCode, ExaminerSelectRequest.From(card.cardData))
                : new WebSocketRequestMessage(WebSocketMessageType.SUBMIT_CARD, GameStatics.RoomCode, SubmitCardRequest.From(card.cardData)));
        }

        public static Task ConnectGame(string roomCode)
        {
            return WebSocketClient.Message(new WebSocketRequestMessage(WebSocketMessageType.CONNECT_GAME, roomCode, null));
        }
    }
}