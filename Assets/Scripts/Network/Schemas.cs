using System;
using System.Collections.Generic;

namespace Network
{
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