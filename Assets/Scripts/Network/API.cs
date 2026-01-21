using System.Threading.Tasks;
using GameLogic;
using Managers;

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

        public static void TimeOver()
        {
            
        }

        public static Task SubmitCard(Card card)
        {
            return WebSocketClient.Message(GameStatics.IsExaminer()
                ? new WebSocketRequestMessage<ExaminerSelectRequest>(WebSocketMessageType.EXAMINER_SELECT, GameStatics.RoomCode, ExaminerSelectRequest.From(card.cardData))
                : new WebSocketRequestMessage<SubmitCardRequest>(WebSocketMessageType.SUBMIT_CARD, GameStatics.RoomCode, SubmitCardRequest.From(card.cardData)));
        }

        public static Task ConnectGame(string roomCode)
        {
            return WebSocketClient.Message(new WebSocketRequestMessage<Void>(WebSocketMessageType.CONNECT_GAME, roomCode, new Void()));
        }
    }
}