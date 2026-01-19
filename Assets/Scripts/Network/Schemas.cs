using System;
using System.Collections.Generic;
using GameLogic;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Unity.VisualScripting;

namespace Network
{
    [Serializable]
    public class Void
    {
    }

    [Serializable]
    public class GameStartData
    {
        public List<ParticipantInfo> participants;
        public QuizResponse question;
    }

    [Serializable]
    public class ParticipantInfo
    {
        public long userId;
        public string nickname;
        public int level;
        public bool isExaminer;
        public int bananaScore;
    }

    [Serializable]
    public class QuizResponse
    {
        public long quizId;
        public string content;
    }

    [Serializable]
    public class SubmitCardRequest
    {
        public long cardId;

        public SubmitCardRequest(long cardId)
        {
            this.cardId = cardId;
        }

        public static SubmitCardRequest From(CardData data)
        {
            return new SubmitCardRequest(data.cardId);
        }
    }

    [Serializable]
    public class CardListResponse
    {
        public List<CardData> cards; // CardData == CardResponse
    }

    [Serializable]
    public class ExaminerSelectRequest
    {
        public long participantId;

        public ExaminerSelectRequest(long participantId)
        {
            this.participantId = participantId;
        }

        public static ExaminerSelectRequest From(CardData data)
        {
            return new ExaminerSelectRequest(data.participantId);
        }
    }

    [Serializable]
    public class MyPageResponse
    {
        public long id;
        public string email;
        public string nickname;
        public int level;
        public int bananaxp;
    }

    [Serializable]
    public class ExaminerSelectionDto
    {
        public long participantId;
        public string cardWord;
        public string winnerNickname;
        public int newBananaScore;
    }

    [Serializable]
    public class NextRoundResponse
    {
        public long newExaminerId;
        public string newExaminerNickname;
        public QuizResponse quiz;
    }

    [Serializable]
    public class ErrorBody
    {
        public string status;
        public string message;
        public string timestamp;
    }

    [Serializable]
    public class WebSocketRequestMessage
    {
        public WebSocketMessageType type;
        public string roomCode;
        public string data;

        public WebSocketRequestMessage(WebSocketMessageType type, string roomCode, [CanBeNull] object data)
        {
            this.type = type;
            this.roomCode = roomCode;
            this.data = JsonConvert.SerializeObject(data);
        }
    }

    [Serializable]
    public class WebSocketMessage
    {
        public WebSocketMessageType type;
        public string roomCode;
        public string data;
        public string message;
    }

    public enum WebSocketMessageType
    {
        CONNECTED,

        CREATE_ROOM,
        ROOM_CREATED,
        JOIN_ROOM,
        ROOM_JOINED,
        USER_JOINED,
        ROOM_EXIT,

        GAME_READY, // 4명 모임 → 게임 서버 연결 안내
        CONNECT_GAME, // 클라이언트 → 게임 서버 연결 요청
        GAME_START, // 게임 시작 (4명 모두 연결 완료 시, 질문 포함)
        SUBMIT_CARD,
        CARD_SUBMITTED,
        ALL_CARDS_SUBMITTED,
        EXAMINER_SELECT, // 출제자가 카드 선택
        EXAMINER_SELECTED, // 출제자 선택 완료
        NEXT_ROUND, // 다음 라운드 시작 (새 출제자 + 새 질문)
        ROUND_END, // 게임 종료 (5점 달성)

        ERROR
    }
}