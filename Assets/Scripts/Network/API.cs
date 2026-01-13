using GameLogic;

namespace Network
{
    public static class API
    {
        public static Networking.Get<RoomResponse> GetRooms()
        {
            return new Networking.Get<RoomResponse>("/play/room");
        }

        public static Networking.Get<Void> GetCards()
        {
            return new Networking.Get<Void>("/play-together/cards");
        }
    }
}