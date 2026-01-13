using System;
using System.Collections.Generic;
using GameLogic;

namespace Network
{
    [Serializable]
    public class Void
    {
    }
    
    [Serializable]
    public class CardListResponse
    {
        public List<CardData> cards; // CardData == CardResponse
    }
    
    [Serializable]
    public class RoomResponse
    {
        public long id;
        public string code;
    }
    
    [Serializable]
    public class Participant
    {
        public long id;
        public User userId;
        public RoomResponse roomId;
        public int bananaScore;
    }

    [Serializable]
    public class User
    {
        public long id;
        public string email;
        public string nickname;
        public string password;
        public int level;
        public int bananaxp;
    }
    
    [Serializable]
    public class ErrorBody
    {
        public int errorId;
        public string message;
    }
    
    [Serializable]
    public class CommandMessage
    {
        public string command;
        public string data;
        
        public CommandMessage(string command, string data)
        {
            this.command = command;
            this.data = data;
        }
    }
}